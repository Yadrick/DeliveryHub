namespace DeliveryHub.Application.Abstractions
{
    /// <summary>
    /// Определяет абстракцию единицы работы для фиксации изменений в данных, сохраняемых в систему.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Сохраняет все ожидающие изменения в бд.
        /// </summary>
        /// <returns>Количество сохраненных(измененных) записей.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
