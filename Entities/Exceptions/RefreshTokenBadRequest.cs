namespace Entities.Exceptions
{
    public sealed class RefreshTokenBadRequest : BadRequestException
    {
        public RefreshTokenBadRequest()
            : base("Invalid client request. The TokenDto has some invalid values.")
        {
        }
    }
}
