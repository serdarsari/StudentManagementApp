
namespace StudentManagement.DTO.BaseClasses
{
    public abstract class GetAllBaseRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
