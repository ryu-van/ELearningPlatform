namespace E_learning_platform.Exceptions
{
    public class EntityNotFoundException :Exception
    {
        public string EntityName { get; }
        public IEnumerable<long> InvalidIds { get; }

        public EntityNotFoundException(string entityName, IEnumerable<long> invalidIds)
            : base($"{entityName} not found with IDs: {string.Join(", ", invalidIds)}")
        {
            EntityName = entityName;
            InvalidIds = invalidIds;
        }
    }
}
