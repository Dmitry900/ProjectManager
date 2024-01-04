namespace ProjectManager.Domain.Entities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }

        public ICollection<BoardEntity> Boards { get; set; } = new List<BoardEntity>();
    }
}
