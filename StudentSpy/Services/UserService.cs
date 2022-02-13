using StudentSpy.WebAPI.Data;

namespace StudentSpy.Services
{
    public class UserService : IUserService
    {
        private AppDbContext context;
        
        private readonly ILogger<UserService> logger;

        public UserService(
            AppDbContext context,            
            ILogger<UserService> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        //public IEnumerable<User> GetAll()
        //{
        //    return context.Users.ToList();
        //}

        //public User GetById(int id)
        //{
        //    var user = context.Users.Find(id);
        //    if (user == null) throw new KeyNotFoundException("User not found");
        //    return user;
        //}

        //public void Register(RegisterRequest model)
        //{
        //    var checkIfUserExists = context.Users.SingleOrDefault(x => x.Email == model.Email);
        //    if(checkIfUserExists != null)
        //    {
        //        logger.LogError("User already exists");
        //        throw new Exceptions("User already exists");
        //    }

        //    var user = new User
        //    {
        //        Name = model.Name,
        //        LastName = model.LastName,
        //        Email = model.Email,
        //        Age = Convert.ToInt32(model.Age),                
        //        Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
        //    };
            
        //    // save changes to db
        //    context.Add(user);
        //    context.SaveChanges();
        //    logger.LogInformation("User created successfully!");
        //}

        //public AuthResponse Authenticate(AuthRequest model, string ipAddress)
        //{
        //    var user = context.Users.SingleOrDefault(x => x.Email == model.Email);
            
        //    // validate
        //    if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        //    {
        //        throw new Exceptions("Username or password is incorrect");
        //    }

        //    // authentication successful so generate jwt and refresh tokens
        //    var jwtToken = jwtUtils.GenerateJwtToken(user);
        //    var refreshToken = jwtUtils.GenerateRefreshToken(ipAddress);
        //    user.RefreshTokens.Add(refreshToken);

        //    // remove old refresh tokens from user
        //    removeOldRefreshTokens(user);

        //    // save changes to db
        //    context.Update(user);
        //    context.SaveChanges();

        //    return new AuthResponse(user, jwtToken, refreshToken.Token);
        //}

        //public AuthResponse RefreshToken(string token, string ipAddress)
        //{
        //    var user = getUserByRefreshToken(token);
        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        //    if (refreshToken.IsRevoked)
        //    {
        //        // revoke all descendant tokens in case this token has been compromised
        //        revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
        //        context.Update(user);
        //        context.SaveChanges();
        //    }

        //    if (!refreshToken.IsActive)
        //        throw new Exceptions("Invalid token");

        //    // replace old refresh token with a new one (rotate token)
        //    var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
        //    user.RefreshTokens.Add(newRefreshToken);

        //    // remove old refresh tokens from user
        //    removeOldRefreshTokens(user);

        //    // save changes to db
        //    context.Update(user);
        //    context.SaveChanges();

        //    // generate new jwt
        //    var jwtToken = jwtUtils.GenerateJwtToken(user);

        //    return new AuthResponse(user, jwtToken, newRefreshToken.Token);
        //}

        //public void RevokeToken(string token, string ipAddress)
        //{
        //    var user = getUserByRefreshToken(token);
        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        //    if (!refreshToken.IsActive)
        //        throw new Exceptions("Invalid token");

        //    // revoke token and save
        //    revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
        //    context.Update(user);
        //    context.SaveChanges();
        //}

        //// helper methods

        //private User getUserByRefreshToken(string token)
        //{
        //    var user = context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

        //    if (user == null)
        //        throw new Exceptions("Invalid token");

        //    return user;
        //}

        //private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        //{
        //    var newRefreshToken = jwtUtils.GenerateRefreshToken(ipAddress);
        //    revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
        //    return newRefreshToken;
        //}

        //private void removeOldRefreshTokens(User user)
        //{
        //    // remove old inactive refresh tokens from user based on TTL in app settings
        //    user.RefreshTokens.RemoveAll(x =>
        //        !x.IsActive &&
        //        x.Created.AddDays(appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        //}

        //private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        //{
        //    // recursively traverse the refresh token chain and ensure all descendants are revoked
        //    if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        //    {
        //        var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
        //        if (childToken.IsActive)
        //            revokeRefreshToken(childToken, ipAddress, reason);
        //        else
        //            revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
        //    }
        //}

        //private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        //{
        //    token.Revoked = DateTime.UtcNow;
        //    token.RevokedByIp = ipAddress;
        //    token.ReasonRevoked = reason;
        //    token.ReplacedByToken = replacedByToken;
        //}
    }
}