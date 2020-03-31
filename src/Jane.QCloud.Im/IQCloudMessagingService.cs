using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jane.QCloud.Im
{
    public interface IQCloudMessagingService
    {
        Task<AccountGetPartialProfileOutput> AccountGetPartialProfileAsync(List<string> input);

        Task AccountImportAsync(AccountImportInput input);

        Task AccountSetPartialProfileAsync(AccountSetPartialProfileInput input);

        Task BatchSendMessageAsync(BatchSendMessageInput input);

        Task DirtyWordsAddAsync(DirtyWordsAddInput input);

        Task DirtyWordsDeleteAsync(DirtyWordsDeleteInput input);

        Task<DirtyWordsGetOutput> DirtyWordsGetAsync();

        Task<string> GetIdentifierSignatureAsync(string identifier);

        Task<GroupAddMemberOutput> GroupAddMemberAsync(GroupAddMemberInput input);

        Task GroupChangeOwnerAsync(GroupChangeOwnerInput input);

        Task<GroupCreateOutput> GroupCreateAsync(GroupCreateInput input);

        Task GroupDeleteMemberAsync(GroupDeleteMemberInput input);

        Task GroupDestroyAsync(GroupDestroyInput input);

        Task<GroupGetDetailResponse> GroupGetDetailAsync(GroupGetDetailInput input);

        Task<GroupGetMembersResponse> GroupGetMembersAsync(GroupGetMembersInput input);

        Task GroupSendSystemNotificationAsync(GroupSendSystemNotificationInput input);

        Task GroupUpdateBaseInfoAsync(GroupUpdateBaseInfoInput input);

        Task RelationshipBlacklistAddAsync(RelationshipBlacklistAddInput input);

        Task RelationshipBlacklistDeleteAsync(RelationshipBlacklistDeleteInput input);

        Task<RelationshipBlacklistGetResponse> RelationshipBlacklistGetAsync(RelationshipBlacklistGetInput input);
    }
}