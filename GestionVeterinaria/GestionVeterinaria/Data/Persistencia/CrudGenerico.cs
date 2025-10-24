using LiteDB;
using GestionVeterinaria.Data;

namespace GestionVeterinaria.Services
{
    public class CrudGenerico<T> where T : class
    {
        protected readonly LiteDbContext _context;
        protected readonly ILiteCollection<T> _collection;

        public CrudGenerico(LiteDbContext context, ILiteCollection<T> collection)
        {
            _context = context;
            _collection = collection;
        }

        public T? ObtenerPorId(int id)
        {
            return _collection.FindById(id);
        }

        public IEnumerable<T> ObtenerTodos()
        {
            return _collection.FindAll();
        }

        public bool Crear(T entidad)
        {
            try
            {
                _collection.Insert(entidad);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(T entidad)
        {
            return _collection.Update(entidad);
        }

        public bool Eliminar(int id)
        {
            return _collection.Delete(id);
        }
    }
}