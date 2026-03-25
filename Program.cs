namespace puntoVenta;

public class Usuario(string nombre, string passwd)
{
    public string nombre = nombre;
    public string passwd = passwd;
    public void iniciarSesion(string nombre, string passwd)
    {
        if (this.nombre == nombre && this.passwd == passwd)
        {
            System.Console.WriteLine("Inicio de sesión exitoso");
        }
        else
        {
            System.Console.WriteLine("Nombre de usuario o contraseña incorrectos");
        }
    }
}
public class Producto(string nombre, double precio)
{
    public string nombre = nombre;
    public double precio = precio;
    static Random rnd = new Random();
    public int stock = rnd.Next(1, 100);
}
public class Factura(Usuario usuario)
{
    private List<Producto> productos = new List<Producto>();
    public void AgregarProducto(Producto producto)
    {
        if (producto.stock > 0)
        {
            productos.Add(producto);
            producto.stock--;
        }
    }
    public double CalcularSubTotal()
    {
        double total = 0;
        foreach (var producto in productos)
        {
            total += producto.precio;
        }
        return total;
    }
    public double CalcularTotal()
    {
        double subTotal = CalcularSubTotal();
        subTotal += subTotal * 0.16;
        return subTotal;
    }
    public void verLsProductos()
    {
        foreach (var producto in productos)
        {
            Console.WriteLine($"Producto: {producto.nombre}, Precio: {producto.precio}");
        }
    }
    public void verFactura()
    {
        Console.WriteLine($"Usuario: {usuario.nombre}");
        Console.WriteLine("Productos:");
        verLsProductos();
        Console.WriteLine($"Subtotal: {CalcularSubTotal()}");
        Console.WriteLine($"Total: {CalcularTotal()}");
    }
    public void eliminarProducto(Producto producto)
    {
        productos.Remove(producto);
        producto.stock++;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Usuario> usuarios = new List<Usuario>();
        List<Producto> productos = new List<Producto>();
        Boolean continuar = true;
        do
        {
            System.Console.WriteLine("Bienvenido al punto de venta");
            System.Console.WriteLine("Qué quiere hacer? (introduzca índice)");
            System.Console.WriteLine("1. Crear usuario");
            System.Console.WriteLine("2. Crear producto");
            System.Console.WriteLine("3. Iniciar sesión");
            System.Console.WriteLine("4. Salir");
            switch (Console.ReadLine())
            {
                case "1":
                    System.Console.WriteLine("Introduzca el nombre del usuario");
                    string nombre = Console.ReadLine();
                    System.Console.WriteLine("Introduzca la contraseña del usuario");
                    string passwd = Console.ReadLine();
                    Usuario usuario = new Usuario(nombre, passwd);
                    System.Console.WriteLine($"Usuario {usuario.nombre} creado exitosamente");
                    usuarios.Add(usuario);
                    break;
                case "2":
                    System.Console.WriteLine("Introduzca el nombre del producto");
                    string nombreProducto = Console.ReadLine();
                    System.Console.WriteLine("Introduzca el precio del producto");
                    double precioProducto = 0.0;
                    bool valPrec = false;
                    do
                    {
                        try
                        {
                            precioProducto = Convert.ToDouble(Console.ReadLine());
                            valPrec = true;
                        }
                        catch
                        {
                            System.Console.WriteLine("Introduzca un precio válido");
                        }
                    } while (!valPrec);
                    Producto producto = new Producto(nombreProducto, precioProducto);
                    System.Console.WriteLine($"Producto {producto.nombre} creado exitosamente con precio {producto.precio}");
                    productos.Add(producto);
                    break;
                case "3":
                    System.Console.WriteLine("Introduzca el nombre del usuario");
                    string nombreLogin = Console.ReadLine();
                    System.Console.WriteLine("Introduzca la contraseña del usuario");
                    foreach (Usuario user in usuarios)
                    {
                        if (user.nombre == nombreLogin)
                        {
                            string passwdLogin = Console.ReadLine();
                            user.iniciarSesion(nombreLogin, passwdLogin);
                            Factura factura = new Factura(user);
                            Boolean continuar2 = true;
                            do
                            {
                                System.Console.WriteLine("Qué quiere hacer? (introduzca índice)");
                                System.Console.WriteLine("1. Comprar producto");
                                System.Console.WriteLine("2. Ver factura");
                                System.Console.WriteLine("3. Eliminar producto de la factura");
                                System.Console.WriteLine("4. Salir");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        System.Console.WriteLine("Introduzca el nombre del producto");
                                        string nombreProductoFactura = Console.ReadLine();
                                        foreach (Producto prod in productos)
                                        {
                                            if (prod.nombre == nombreProductoFactura)
                                            {
                                                factura.AgregarProducto(prod);
                                                System.Console.WriteLine($"Producto {prod.nombre} agregado a la factura");
                                                break;
                                            }
                                        }
                                        break;
                                    case "2":
                                        factura.verFactura();
                                        break;
                                    case "3":
                                        System.Console.WriteLine("Introduzca el nombre del producto");
                                        string nombreProductoEliminar = Console.ReadLine();
                                        foreach (Producto prod in productos)
                                        {
                                            if (prod.nombre == nombreProductoEliminar)
                                            {
                                                factura.eliminarProducto(prod);
                                                System.Console.WriteLine($"Producto {prod.nombre} eliminado de la factura");
                                                break;
                                            }
                                        }
                                        break;
                                    case "4":
                                        continuar2 = false;
                                        break;
                                    default:
                                        System.Console.WriteLine("Opción no válida");
                                        break;
                                }
                            } while(continuar2);
                        }
                    }
                    break;
                case "4":
                    continuar = false;
                    break;
                default:
                    System.Console.WriteLine("Opción no válida");
                    break;
            }
        } while (continuar);
    }
}