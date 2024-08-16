namespace login.Domain.Dtos.AccountDtos
{
    public class UserRegisterDto : UserLoginDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}