using Tasker.Web.Data;

namespace Tasker.Web.Features.Organizations;

public record OrganizationDto(Guid Id, string Name, OrganizationMemberRole UserRole);