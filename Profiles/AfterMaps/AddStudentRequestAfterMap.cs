using AutoMapper;
using SchoolApplication.DomainModels;

namespace SchoolApplication.Profiles.AfterMaps
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, Models.Student>
    {
        public void Process(AddStudentRequest source, Models.Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.address = new Models.Address()
            {
                Id=Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
