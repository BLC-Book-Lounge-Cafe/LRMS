using LRMS.Application.Exceptions;
using LRMS.Application.TableReservations;
using LRMS.Application.TableReservations.Dto;
using LRMS.Application.TableReservations.Requests;
using System.Text.RegularExpressions;

namespace LRMS.Application.Services;

public partial class TableReservationService(ITableReservationRepository repository) : ITableReservationService
{
    private readonly ITableReservationRepository _repository = repository;

    public async Task CreateTableReservation(TableReservationDto tableReservationDto, CancellationToken ct = default)
    {
        if (tableReservationDto.CustomerName.Length > 255)
            throw new DomainException("Имя клиента слишком длинное. Максимальное количество символов 255.");

        if (!CustomerNumberRegex().IsMatch(tableReservationDto.CustomerPhone))
            throw new DomainException("Номер клиента не соответствует формату.");

        if (tableReservationDto.StartTime > tableReservationDto.EndTime)
            throw new DomainException("Начальное время бронирования не может превышать конечное.");

        await _repository.CreateTableReservation(tableReservationDto, ct);
    }

    [GeneratedRegex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")]
    private static partial Regex CustomerNumberRegex();

    public async Task<GetTableReservationSlotsResponse> GetSlots(GetTableReservationSlotsRequest request, CancellationToken ct = default)
    {
        return new(await _repository.GetSlots(request.TableId, request.Date, ct));
    }
}
