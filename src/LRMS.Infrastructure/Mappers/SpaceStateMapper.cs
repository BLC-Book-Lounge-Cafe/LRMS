using LRMS.Application.Dto;
using LRMS.Infrastructure.Persistence.Models;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class SpaceStateMapper
{
    [MapperIgnoreSource(nameof(SpaceStateEntity.Id))]
    [MapperIgnoreSource(nameof(SpaceStateEntity.UpdatedAt))]
    [MapperIgnoreSource(nameof(SpaceStateEntity.CurrentTrack))]
    [MapperIgnoreTarget(nameof(SpaceStateDto.WorkloadLevel))]
    [MapperIgnoreTarget(nameof(SpaceStateDto.CurrentTrack))]
    public static partial SpaceStateDto ToDto(SpaceStateEntity entity);
}
