using MediatR;
using StudentManagement.DTO.ParentDTO;

namespace StudentManagement.Service.Core.Features.Commands.CreateParent
{
    public partial class CreateParentCommand : IRequest<CreateParentResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
