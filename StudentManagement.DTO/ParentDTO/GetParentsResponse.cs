
namespace StudentManagement.DTO.ParentDTO
{
    public class GetParentsResponse
    {
        public int TotalParents { get; set; }
        public string NextPage { get; set; }
        public List<ParentResponse> Parents { get; set; }
    }
}
