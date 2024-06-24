// Exemplo de controlador de conta
using Loja.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly LojaContext _context;

    public AccountController(LojaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string user, string senha)
    {
        var usuario = await _context.Usuario.SingleOrDefaultAsync(u => u.Username == user && u.Password == senha);
        if (usuario != null)
        {
            // Definir cookies de autenticação ou usar Identity
            HttpContext.Session.SetString("UsuarioId", usuario.UsuarioId.ToString());
            HttpContext.Session.SetString("Role", usuario.Role);
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError("", "Login inválido");
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

// Exemplo de filtro de autorização
public class AuthorizeRoleAttribute : AuthorizeAttribute
{
    private readonly string _role;

    public AuthorizeRoleAttribute(string role)
    {
        _role = role;
    }

    public virtual void OnAuthorization(AuthorizationFilterContext context)
    {
        var role = context.HttpContext.Session.GetString("Role");
        if (role != _role)
        {
            context.Result = new ForbidResult();
        }
    }
    
}
