using MediatR;
using StudentManagement.DTO.ManagerDTO;

namespace StudentManagement.Service.Core.Features.Commands.CreateManager
{
    public partial class CreateManagerCommand : IRequest<CreateManagerResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
