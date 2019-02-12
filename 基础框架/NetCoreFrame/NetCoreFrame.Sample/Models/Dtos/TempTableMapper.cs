using AutoMapper;
using DapperExtensions.Mapper;

namespace NetCoreFrame.Sample
{
    //public class TempTableMapper : ClassMapper<TempTable>
    //{
    //    public TempTableMapper()
    //    {
    //        Table("TempTable");
    //        Map(x => x.TextData).Ignore();
    //        AutoMap();
    //    }
    //}

    //public class SourceProfile : Profile
    //{

    //    protected override void Configure()
    //    {
    //        //Source->Destination
    //        CreateMap<FieldInfo, FieldInfoDto>();

    //        //Source->Destination2
    //        CreateMap<FieldInfo, FieldInfoDto>().ForMember(
    //            d => d.TableName,
    //            opt =>
    //                {
    //                    opt.MapFrom(s => s.FieldName);
    //                }
    //        );
    //    }
    //}

}