using Refit;
using System.Threading.Tasks;

namespace Jane.QCloud.Im
{
    public interface IQCloudMessagingApi
    {
        [Post("/v4/profile/portrait_get")]
        Task<AccountGetProfileResponse> AccountGetProfileAsync(DefaultQCloudMessagingParameter parameter, [Body]AccountGetProfileInput input);

        [Post("/v4/im_open_login_svc/account_import")]
        Task<QCloudResponse> AccountImportAsync(DefaultQCloudMessagingParameter parameter, [Body]AccountImportInput input);

        [Post("/v4/profile/portrait_set")]
        Task<QCloudResponse> AccountSetProfileAsync(DefaultQCloudMessagingParameter parameter, [Body]AccountSetProfileInput input);

        [Post("/v4/openim/batchsendmsg")]
        Task<QCloudResponse> BatchSendMessageAsync(DefaultQCloudMessagingParameter parameter, [Body]BatchSendMessageInput input);

        [Post("/v4/openim_dirty_words/add")]
        Task<QCloudResponse> DirtyWordsAddAsync(DefaultQCloudMessagingParameter parameter, [Body]DirtyWordsAddInput input);

        [Post("/v4/openim_dirty_words/get")]
        Task<DirtyWordsGetResponse> DirtyWordsGetAsync(DefaultQCloudMessagingParameter parameter, [Body]DirtyWordsAddInput input);

        [Post("/v4/openim_dirty_words/delete")]
        Task<QCloudResponse> DirtyWrodsDeleteAsync(DefaultQCloudMessagingParameter parameter, [Body]DirtyWordsDeleteInput input);

        [Post("/v4/group_open_http_svc/add_group_member")]
        Task<GroupAddMemberResponse> GroupAddMemberAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupAddMemberInput input);

        [Post("/v4/group_open_http_svc/change_group_owner")]
        Task<QCloudResponse> GroupChangeOwnerAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupChangeOwnerInput input);

        [Post("/v4/group_open_http_svc/create_group")]
        Task<GroupCreateResponse> GroupCreateAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupCreateInput input);

        [Post("/v4/group_open_http_svc/delete_group_member")]
        Task<QCloudResponse> GroupDeleteMemberAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupDeleteMemberInput input);

        [Post("/v4/group_open_http_svc/destroy_group")]
        Task<QCloudResponse> GroupDestroyAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupDestroyInput input);

        [Post("/v4/group_open_http_svc/get_group_info")]
        Task<GroupGetDetailResponse> GroupGetDetailAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupGetDetailInput input);

        [Post("/v4/group_open_http_svc/get_group_member_info")]
        Task<GroupGetMembersResponse> GroupGetMembersAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupGetMembersInput input);

        [Post("/v4/group_open_http_svc/send_group_system_notification")]
        Task<QCloudResponse> GroupSendSystemNotificationAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupSendSystemNotificationInput input);

        [Post("/v4/group_open_http_svc/modify_group_base_info")]
        Task<QCloudResponse> GroupUpdateBaseInfoAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupUpdateBaseInfoInput input);

        [Post("/v4/group_open_http_svc/modify_group_member_info")]
        Task<QCloudResponse> GroupUpdateMemeberInfoAsync(DefaultQCloudMessagingParameter parameter, [Body]GroupUpdateMemeberInput input);

        [Post("/v4/sns/black_list_add")]
        Task<RelationshipBlacklistAddResponse> RelationshipBlacklistAddAsync(DefaultQCloudMessagingParameter parameter, [Body]RelationshipBlacklistAddInput input);

        [Post("/v4/sns/black_list_delete")]
        Task<RelationshipBlacklistDeleteResponse> RelationshipBlacklistDeleteAsync(DefaultQCloudMessagingParameter parameter, [Body]RelationshipBlacklistDeleteInput input);

        [Post("/v4/sns/black_list_get")]
        Task<RelationshipBlacklistGetResponse> RelationshipBlacklistGetAsync(DefaultQCloudMessagingParameter parameter, [Body]RelationshipBlacklistGetInput input);
    }
}