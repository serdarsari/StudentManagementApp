using MediatR;
using StudentManagement.DTO.BaseClasses;
using StudentManagement.DTO.TeacherDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Service.Core.Features.Queries.GetTeachers
{
    public partial class GetTeachersQuery : GetAllBaseRequest, IRequest<GetTeachersResponse>
    {
    }
}
