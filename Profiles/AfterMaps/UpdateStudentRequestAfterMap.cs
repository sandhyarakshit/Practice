using AutoMapper;
using SchoolApplication.DomainModels;
using Models = SchoolApplication.Models;

namespace SchoolApplication.Profiles.AfterMaps
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, Models.Student>
    {
        public void Process(UpdateStudentRequest source, Models.Student destination, ResolutionContext context)
        {
            destination.address = new Models.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
