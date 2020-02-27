using Jane.Configurations;
using Jane.Extensions;
using Jane.Logging;
using Jane.Runtime.Caching;
using Jane.Runtime.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tencentyun;

namespace Jane.QCloud.Im
{
    public class QCloudMessagingService : IQCloudMessagingService
    {
        private readonly ICacheManager _cacheManager;
        private readonly int _groupMembersSubmitLimit = 400;
        private readonly ILogger _logger;
        private readonly IQCloudMessagingApi _messagingApi;
        private readonly QCloudMessagingOptions _options;
        private readonly TLSSigAPIv2 _tlsSigner;

        public QCloudMessagingService(
            JaneMemoryCacheManager cacheManager,
            ILoggerFactory loggerFactory,
            IQCloudMessagingApi messagingApi,
            IOptions<QCloudMessagingOptions> optionsAccessor
            )
        {
            _cacheManager = cacheManager;
            _logger = loggerFactory.Create(nameof(QCloudMessagingService));
            _messagingApi = messagingApi;
            _options = optionsAccessor.Value;
            _tlsSigner = new TLSSigAPIv2(_options.AppId, _options.AppSecret);
        }

        public async Task<AccountGetPartialProfileOutput> AccountGetPartialProfileAsync(List<string> input)
        {
            var response = await _messagingApi.AccountGetProfileAsync(await BuildAdminMessagingParameterAsync(), new AccountGetProfileInput()
            {
                Identifiers = input,
                TagList = new List<string>()
                {
                    "Tag_Profile_IM_Nick",
                    "Tag_Profile_IM_Image"
                }
            });
            ProcessQCloudMessagingResponse(response, input, "portrait_get");

            return new AccountGetPartialProfileOutput()
            {
                AccountProfiles = response.UserProfileItem.Select(p => new AccountImportInput
                {
                    Identifier = p.Identifier,
                    Nick = p.ProfileItem?.FirstOrDefault(i => i.Tag == "Tag_Profile_IM_Nick")?.Value,
                    FaceUrl = p.ProfileItem?.FirstOrDefault(i => i.Tag == "Tag_Profile_IM_Image")?.Value
                }).ToList()
            };
        }

        public async Task AccountImportAsync(AccountImportInput input)
        {
            var response = await _messagingApi.AccountImportAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "account_import");
        }

        public async Task AccountSetPartialProfileAsync(AccountSetPartialProfileInput input)
        {
            var setInput = new AccountSetProfileInput()
            {
                Identifier = input.Identifier,
                ProfileItem = new List<ProfileItem>()
            };

            if (input.Nick == null && input.FaceUrl == null)
            {
                return;
            }

            if (input.Nick != null)
            {
                setInput.ProfileItem.Add(new ProfileItem()
                {
                    Tag = "Tag_Profile_IM_Nick",
                    Value = input.Nick
                });
            }
            if (input.FaceUrl != null)
            {
                setInput.ProfileItem.Add(new ProfileItem()
                {
                    Tag = "Tag_Profile_IM_Image",
                    Value = input.FaceUrl
                });
            }

            var response = await _messagingApi.AccountSetProfileAsync(await BuildAdminMessagingParameterAsync(), setInput);
            ProcessQCloudMessagingResponse(response, input, "portrait_set");
        }

        public async Task BatchSendMessageAsync(BatchSendMessageInput input)
        {
            var response = await _messagingApi.BatchSendMessageAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "batchsendmsg", 90012);
        }

        public async Task DirtyWordsAddAsync(DirtyWordsAddInput input)
        {
            var response = await _messagingApi.DirtyWordsAddAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "openim_dirty_words_add");
        }

        public async Task DirtyWordsDeleteAsync(DirtyWordsDeleteInput input)
        {
            var response = await _messagingApi.DirtyWrodsDeleteAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "openim_dirty_words_delete");
        }

        public async Task<DirtyWordsGetOutput> DirtyWordsGetAsync()
        {
            var response = await _messagingApi.DirtyWordsGetAsync(await BuildAdminMessagingParameterAsync(), new DirtyWordsAddInput());
            return new DirtyWordsGetOutput()
            {
                DirtyWordsList = response.DirtyWordsList
            };
        }

        public async Task<string> GetIdentifierSignatureAsync(string identifier)
        {
            return await _cacheManager
                  .GetCache<string, string>("QCloudImSignature")
                  .GetAsync(identifier, () => Task.FromResult(_tlsSigner.GenSig(identifier)));
        }

        public async Task<GroupAddMemberOutput> GroupAddMemberAsync(GroupAddMemberInput input)
        {
            var groups = await GroupGetDetailAsync(new GroupGetDetailInput()
            {
                GroupIds = new List<string>() { input.GroupId }
            });

            groups.GroupInfo = groups.GroupInfo?.Where(g => !g.Name.IsNullOrEmpty()).ToList();

            if (groups.GroupInfo == null || groups.GroupInfo.Count == 0)
            {
                _logger.Warn($"add members[{string.Join(", ", input.MemberList.Select(m => m.Member))}] to group[{input.GroupId}] failed, group not found.");

                return new GroupAddMemberOutput()
                {
                    Success = true
                };
            }

            var currentGroup = groups.GroupInfo.FirstOrDefault();
            //AVChatRoom not need import members
            if (currentGroup.Type == "AVChatRoom")
            {
                return new GroupAddMemberOutput()
                {
                    Success = true
                };
            }

            var skip = 0;
            var output = new GroupAddMemberOutput();

            while (true)
            {
                var members = input.MemberList.Skip(skip).Take(_groupMembersSubmitLimit).ToList();
                if (members.Count == 0)
                {
                    break;
                }

                var addMemberInput = new GroupAddMemberInput() { GroupId = input.GroupId, MemberList = members, Silence = input.Silence };
                var response = await _messagingApi.GroupAddMemberAsync(await BuildAdminMessagingParameterAsync(), addMemberInput);
                ProcessQCloudMessagingResponse(response, input, "add_group_member", 10019);

                if (response.MemberList.Any(m => m.Result == 0))
                {
                    output.Success = false;
                    output.FailedMembers.AddRange(response.MemberList.Where(m => m.Result == 0).Select(m => m.Member).ToList());
                }

                skip += members.Count;
            }

            //update member role
            var adminMembers = input.MemberList.Where(m => m.Role == "Admin");
            foreach (var item in adminMembers)
            {
                var groupUpdateMemberResponse = await _messagingApi.GroupUpdateMemeberInfoAsync(await BuildAdminMessagingParameterAsync(), new GroupUpdateMemeberInput()
                {
                    GroupId = input.GroupId,
                    Member = item.Member,
                    Role = item.Role
                });

                ProcessQCloudMessagingResponse(groupUpdateMemberResponse, input, "modify_group_member_info");
            }

            return output;
        }

        public async Task GroupChangeOwnerAsync(GroupChangeOwnerInput input)
        {
            var groups = await GroupGetDetailAsync(new GroupGetDetailInput()
            {
                GroupIds = new List<string>() { input.GroupId }
            });

            groups.GroupInfo = groups.GroupInfo?.Where(g => !g.Name.IsNullOrEmpty()).ToList();

            if (groups.GroupInfo == null || groups.GroupInfo.Count == 0)
            {
                _logger.Warn($"change group[{input.GroupId}] owner failed, group not found.");

                return;
            }

            var currentGroup = groups.GroupInfo.FirstOrDefault();
            //AVChatRoom can not change owner
            if (currentGroup.Type == "AVChatRoom")
            {
                return;
            }

            var response = await _messagingApi.GroupChangeOwnerAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "change_group_owner");
        }

        public async Task<GroupCreateOutput> GroupCreateAsync(GroupCreateInput input)
        {
            var output = new GroupCreateOutput();

            //AVChatRoom not need import members
            if (input.Type == "AVChatRoom")
            {
                input.MemberList = new List<GroupMember>();
            }

            var allMembers = input.MemberList.ToList();
            var createGroupMembers = allMembers.Take(_groupMembersSubmitLimit).ToList();

            //create group
            input.MemberList = createGroupMembers;
            var response = await _messagingApi.GroupCreateAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "create_group", 10021);
            if (response.MemberList.Any(m => m.Result == 0))
            {
                output.Success = false;
                output.FailedMembers.AddRange(response.MemberList.Where(m => m.Result == 0).Select(m => m.Member).ToList());
            }

            //add members
            var leftMembers = allMembers.Skip(createGroupMembers.Count).ToList();
            if (leftMembers.Count > 0)
            {
                var groupAddMemberOutput = await GroupAddMemberAsync(new GroupAddMemberInput()
                {
                    GroupId = input.GroupId,
                    MemberList = leftMembers
                });

                if (!groupAddMemberOutput.Success)
                {
                    output.Success = false;
                    output.FailedMembers.AddRange(groupAddMemberOutput.FailedMembers);
                }
            }

            return output;
        }

        public async Task GroupDeleteMemberAsync(GroupDeleteMemberInput input)
        {
            var groups = await GroupGetDetailAsync(new GroupGetDetailInput()
            {
                GroupIds = new List<string>() { input.GroupId }
            });

            groups.GroupInfo = groups.GroupInfo?.Where(g => !g.Name.IsNullOrEmpty()).ToList();

            if (groups.GroupInfo == null || groups.GroupInfo.Count == 0)
            {
                _logger.Warn($"delete members[{string.Join(", ", input.MemeberList)}] from group[{input.GroupId}] failed, group not found.");

                return;
            }

            var currentGroup = groups.GroupInfo.FirstOrDefault();
            //AVChatRoom not need import members
            if (currentGroup.Type == "AVChatRoom")
            {
                return;
            }

            var response = await _messagingApi.GroupDeleteMemberAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "delete_group_member", 10007);
        }

        public async Task GroupDestroyAsync(GroupDestroyInput input)
        {
            var response = await _messagingApi.GroupDestroyAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "destroy_group");
        }

        public async Task<GroupGetDetailResponse> GroupGetDetailAsync(GroupGetDetailInput input)
        {
            var response = await _messagingApi.GroupGetDetailAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "get_group_info");
            return response;
        }

        public async Task<GroupGetMembersResponse> GroupGetMembersAsync(GroupGetMembersInput input)
        {
            var response = await _messagingApi.GroupGetMembersAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "get_group_member_info");
            return response;
        }

        public async Task GroupSendSystemNotificationAsync(GroupSendSystemNotificationInput input)
        {
            var response = await _messagingApi.GroupSendSystemNotificationAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "send_group_system_notification");
        }

        public async Task GroupUpdateBaseInfoAsync(GroupUpdateBaseInfoInput input)
        {
            var response = await _messagingApi.GroupUpdateBaseInfoAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "modify_group_base_info");
        }

        public async Task RelationshipBlacklistAddAsync(RelationshipBlacklistAddInput input)
        {
            var response = await _messagingApi.RelationshipBlacklistAddAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "black_list_add");
        }

        public async Task RelationshipBlacklistDeleteAsync(RelationshipBlacklistDeleteInput input)
        {
            var response = await _messagingApi.RelationshipBlacklistDeleteAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "black_list_delete");
        }

        public async Task<RelationshipBlacklistGetResponse> RelationshipBlacklistGetAsync(RelationshipBlacklistGetInput input)
        {
            var response = await _messagingApi.RelationshipBlacklistGetAsync(await BuildAdminMessagingParameterAsync(), input);
            ProcessQCloudMessagingResponse(response, input, "black_list_get");
            return response;
        }

        private async Task<DefaultQCloudMessagingParameter> BuildAdminMessagingParameterAsync()
        {
            var identifier = "admin";

            return new DefaultQCloudMessagingParameter()
            {
                AppId = _options.AppId,
                Identifier = identifier,
                Signature = await GetIdentifierSignatureAsync(identifier)
            };
        }

        private void ProcessQCloudMessagingResponse<T>(T response, object request, string method, params int[] exceptedErrorCode)
            where T : QCloudResponse
        {
            if (response.ActionStatus != "OK")
            {
                if (exceptedErrorCode != null && exceptedErrorCode.Contains(response.ErrorCode))
                {
                    return;
                }

                throw new UserFriendlyException($"request qlcoud im api[{method}] body[{JsonConvert.SerializeObject(request)}] error, error code[{response.ErrorCode}], error info[{response.ErrorInfo}]. ");
            }
        }
    }
}