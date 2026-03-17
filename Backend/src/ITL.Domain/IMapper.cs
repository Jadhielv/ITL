namespace ITL.Domain;

public interface IMapper
{
    TDestination Map<TDestination>(object source);
}
