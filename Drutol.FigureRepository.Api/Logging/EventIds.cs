namespace Drutol.FigureRepository.Api.Logging
{
    public enum EventIds
    {
        FiguresFetched = 10,

        DownloadPackageCreated = 20,
        DownloadPackageCreationFailed = 21,

        TransactionCompleted = 30,
        TransactionFailed = 31,

        OrderCreated = 40,
        OrderCreationFailed = 41,

        AuthSessionCreated = 50,
        AuthSessionCreationFailed = 51,
        AuthSessionAuthenticated = 52,
        AuthSessionAuthenticationFailed = 53,

        AdminAuth = 60,

        LoopringError = 900,
        DatabaseError = 901,
        ConfigurationError = 902,
    }

}
