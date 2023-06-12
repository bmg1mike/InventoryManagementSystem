namespace Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<PaginatedList<Product>, PaginatedList<GetProductssDto>>().ReverseMap();
        CreateMap<Product, GetProductssDto>().ReverseMap();
    }
}