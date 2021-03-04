namespace Adventure.Feature.EventDetailsProvider.Utilities.Interfaces
{
    /// <summary>
    /// Reads sections from stub event data provider.
    /// </summary>
    public interface IEventSourceReader
    {
        /// <summary>
        /// Reads one section.
        /// </summary>
        /// <param name="sectionName">Equals to field name of event.</param>
        /// <returns>Value to be used for creating new random event.</returns>
        string ReadSourceSectionValue(string sectionName);
    }
}
