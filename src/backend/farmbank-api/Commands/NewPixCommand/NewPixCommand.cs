using farmbank_api.Dtos;
using MediatR;

namespace farmbank_api.Commands;

public record NewPixCommand(string UserName, string UserPhone, decimal Value) : IRequest<PixCreated>;