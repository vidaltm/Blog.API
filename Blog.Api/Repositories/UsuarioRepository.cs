using Blog.Api.Models;
using Blog.Api.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Api.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuario Get(string userName, string password)
        {
            var usuarios = new List<Usuario>();

            usuarios.Add(new Usuario { Id = 1, UserName = "Thiago", Password = "thiago123", Role = "admin" });
            usuarios.Add(new Usuario { Id = 2, UserName = "Usuario1", Password = "usuario123", Role = "usuario" });

            return usuarios.Where(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
