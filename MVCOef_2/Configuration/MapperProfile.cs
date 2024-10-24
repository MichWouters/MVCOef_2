namespace Interimkantoor.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Job, JobDetailsViewModel>()
                .ForMember(dest => dest.Beschrijving, x => x.MapFrom(src => src.Omschrijving));

        }
    }
}