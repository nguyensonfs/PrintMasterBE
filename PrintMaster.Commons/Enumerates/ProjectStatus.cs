namespace PrintMaster.Commons.Enumerates
{
    public enum ProjectStatus
    {
        Initialization = 0, // Khởi tạo
        Designing = 1, // Đang thiết kế
        AwaitingApproval = 2, // Chờ phê duyệt
        Approved = 3, // Đã phê duyệt
        InProgress = 4, // Đang thực hiện
        Completed = 5, // Hoàn thành
        Delivered = 6, // Đã giao
        Cancelled = 7, // Đã hủy
        Refuse = 8 // Không duyệt
    }
}
