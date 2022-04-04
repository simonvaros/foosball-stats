using Database.Entities;

namespace FoosballApi.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PlayerModel(Player entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}