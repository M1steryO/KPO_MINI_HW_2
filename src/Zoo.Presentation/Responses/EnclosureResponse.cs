using Domain.Entities;
using System.Linq;
using Zoo.Presentation.Responses;

namespace Zoo.Presentation.Dto
{
    public record EnclosureResponse(
        Guid Id,
        string Type,
        double Size,
        int MaxCapacity,
        int CurrentAnimalsCount,
        IEnumerable<AnimalResponse>? Animals = null)
    {
        public static EnclosureResponse FromDomain(
            Enclosure enclosure,
            IEnumerable<Animal>? animals = null) =>
            new(
                Id: enclosure.Id,
                Type: enclosure.Type.ToString(),
                Size: enclosure.Size,
                MaxCapacity: enclosure.Capacity.Max,
                CurrentAnimalsCount: enclosure.CurrentCount,
                Animals: animals?
    .Where(a => a.EnclosureId == enclosure.Id)
    .Select((Animal a) => AnimalResponse.FromDomain(a))

            );
    }
}
