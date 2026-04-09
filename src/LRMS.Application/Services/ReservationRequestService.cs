using LRMS.Application.Commands;
using LRMS.Application.Exceptions;
using LRMS.Application.Repositories;
using LRMS.Application.Requests;
using System.Text.RegularExpressions;

namespace LRMS.Application.Services;

public partial class ReservationRequestService(IReservationRequestRepository repository) : IReservationRequestService
{
    private readonly IReservationRequestRepository _repository = repository;

    public async Task CreateReservationRequest(CreateReservationRequestCommand command, CancellationToken ct = default)
    {
        if (command.CustomerName.Length > 255)
            throw new DomainException("Имя клиента слишком длинное. Максимальное количество символов 255.");

        if (!CustomerNumberRegex().IsMatch(command.CustomerPhone))
            throw new DomainException("Номер клиента не соответствует формату.");

        await _repository.CreateReservationRequest(command.CustomerName, command.CustomerPhone, ct);
    }

    public async Task DeleteReservationRequest(DeleteReservationRequestCommand command, CancellationToken ct = default)
    {
        await _repository.DeleteReservationRequest(command.Id, ct);
    }

    public async Task<GetReservationRequestsResponse> GetReservationRequests(CancellationToken ct = default)
    {
        return new(await _repository.GetReservationRequests(ct));
    }

    [GeneratedRegex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")]
    private static partial Regex CustomerNumberRegex();
}
