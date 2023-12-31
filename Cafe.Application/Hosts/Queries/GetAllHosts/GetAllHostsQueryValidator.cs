using FluentValidation;

namespace Cafe.Application.Hosts.Queries.GetAllHosts;

public class GetAllHostsQueryValidator : AbstractValidator<GetAllHostsQuery>
{
    public GetAllHostsQueryValidator()
    {
    }
}
