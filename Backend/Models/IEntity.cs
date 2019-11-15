namespace Models
{
    /// <summary>
    ///     MarkerInterface for EfCore Entities
    /// </summary>
    public interface IEntity<T>
    {
        /// <summary>
        ///     PrimaryKey
        /// </summary>
        T Id { get; set; }
    }
}