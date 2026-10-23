
namespace Design.Pattern.Singleton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Singleton.GetSingleton.LogMessage("Request Message from 1");
            Singleton.GetSingleton.LogMessage("Request Message from 2");

            Console.ReadLine();
        }
    }


    // keyword 'sealed' means the class cannot be inherited
    sealed class Singleton
    {
        static int instanceCounter = 0;
        private static Singleton? instance = null;

        private Singleton()
        {
            instanceCounter++;
            Console.WriteLine($"Instances created {instanceCounter}");
        }

        public static Singleton GetSingleton
        {
            get
            {
                instance ??= new Singleton();

                return instance;
            }
        }

        public void LogMessage(string message)
        {
            Console.WriteLine($"Message: {message}");
        }
    }
}

namespace Design.Pattern.Factory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? vehicleType = null;
            Console.WriteLine("Enter a vehicle type: car, bike, rickshaw");
            vehicleType = Console.ReadLine();

            IVehicle? vehicle = VehicleFactory.Create(vehicleType);
            if (vehicle != null)
            {
                Console.WriteLine($"Type is {vehicle.VehicleType}");
                Console.WriteLine($"Number of wheels is {vehicle.NumberOfWheels()}");
            }

            Console.ReadKey();
        }
    }

    public interface IVehicle
    {
        public string VehicleType { get; }

        public int NumberOfWheels();
    }

    public class Bike : IVehicle
    {
        private readonly int _wheels;

        public Bike()
        {
            this._wheels = 2;
        }

        public string VehicleType { get => "Bike class"; }

        public int NumberOfWheels()
        {
            return this._wheels;
        }
    }

    public class Car : IVehicle
    {
        private readonly int _wheels;

        public Car()
        {
            this._wheels = 4;
        }

        public string VehicleType { get => "Car class"; }

        public int NumberOfWheels() => this._wheels;
    }

    public class Rickshaw : IVehicle
    {
        private readonly int _wheels;

        public Rickshaw()
        {
            this._wheels = 3;
        }

        public string VehicleType { get => "Car class"; }

        public int NumberOfWheels() => this._wheels;
    }

    public class VehicleFactory
    {
        public static IVehicle? Create(string? type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            IVehicle? vehicle = null;

            switch (type.ToLower())
            {
                case "bike":
                    vehicle = new Bike();
                    break;
                case "car":
                    vehicle = new Car();
                    break;
                case "rickshaw":
                    vehicle = new Rickshaw();
                    break;
                default:
                    break;
            }

            return vehicle;
        }
    }
}

namespace Design.Pattern.Functional
{
    /* ====
     * Functional programming is a declarative programming paradigm style 
     * where one applies pure functions in sequence to solve complex problems.
     * -- Functions are first-class citizens
     * -- Functions are pure function and have no side effects (must not mutate a object OUTSIDE the function).
     * -- Function are immutable and doesn't not change state of objects, only creates new object or value.
     * -- Recursion instead of loops - function invoking themselves ?? hmm... could add to call-stack until base case is met ... stack overflow error ??
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            // == Functional Style paradigm using Classes ==
            Print("Food - products total less than $100");
            Print(
                Product.Sum(
                    Product.AmountRangeFilter(
                        Product.CategoryFilter(
                            Product.GetProducts(), Category.Food
                        ), 0m, 100m
                    )
                )
            );

            // == Functional Style paradigm using Classes ==
            Print("Food - products total between $25 and $75.50 with charity donation");
            var products = Product.GetProducts();
            products = Product.AmountRangeFilter(products, 25.0m, 75.5m);
            products = Product.CategoryFilter(products, Category.Food);
            decimal sum = Product.Sum(products);
            sum = Product.RoundUpForCharity(sum);

            Print(sum);

            // == Functional Style paradigm using Classes ==
            Print("Food - products total between $25 and $75.50 with store and customer discount");
            products = Product.GetProducts();
            products = Product.AmountRangeFilter(products, 25.0m, 75.5m);
            products = Product.CategoryFilter(products, Category.Food);
            products = Product.ApplyStoreDiscount(products, clothingDiscount: 0.0m, foodDiscount: 8.0m);
            products = Product.ApplyCustomerDiscount(products, 11.0m);
            sum = Product.Sum(products);

            Print(sum);


            // == Imperative Style paradigm ==
            Print("Clothing - products total less than $50.00 with charity donation");
            var productSum = new ChainableProduct()
                .GetProducts()
                .CategoryFilter(Category.Clothing)
                .AmountRangeFilter(min: 0m, max: 50.0m)
                .Sum
                .RoundUpForCharity();

            Print(productSum);

            Console.ReadKey();
        }

        public static void Print(object value) => Console.WriteLine(value.ToString());


    }



    public class Product
    {
        public static Object[] GetProducts()
        {
            return ServiceLayer.GetProducts();
        }

        public static Object[] CategoryFilter(Object[] products, string filter)
        {
            var obj = Array.Empty<Object>();
            for (int i = 0; i < products.Length; i++)
            {
                var item = (dynamic)products[i];
                if (item.category == filter)
                {
                    Array.Resize(ref obj, obj.Length + 1);
                    obj[^1] = products[i];
                }
            }
            return obj;
        }

        public static Object[] AmountRangeFilter(Object[] products, decimal min, decimal max)
        {
            var obj = Array.Empty<Object>();
            for (int i = 0; i < products.Length; i++)
            {
                var item = (dynamic)products[i];
                if (item.amount >= min && item.amount <= max)
                {
                    Array.Resize(ref obj, obj.Length + 1);
                    obj[^1] = products[i];
                }
            }
            return obj;
        }

        public static Object[] ApplyStoreDiscount(Object[] products, decimal clothingDiscount, decimal foodDiscount)
        {
            var obj = Array.Empty<Object>();
            for (int i = 0; i < products.Length; i++)
            {
                var item = (dynamic)products[i];

                decimal amount = 0.0m;
                switch (item.category)
                {
                    case Category.Food:
                        amount = Math.Round(item.amount - (item.amount * (foodDiscount / 100)), 2);
                        break;
                    case Category.Clothing:
                        amount = Math.Round(item.amount - (item.amount * (clothingDiscount / 100)), 2);
                        break;
                    default:
                        break;
                }

                Array.Resize(ref obj, obj.Length + 1);
                obj[^1] = new { item.name, item.category, amount };
            }
            return obj;
        }

        public static Object[] ApplyCustomerDiscount(Object[] products, decimal discount)
        {
            var obj = Array.Empty<Object>();
            for (int i = 0; i < products.Length; i++)
            {
                var item = (dynamic)products[i];
                decimal amount = Math.Round(item.amount - (item.amount * (discount / 100)), 2);

                Array.Resize(ref obj, obj.Length + 1);
                obj[^1] = new { item.name, item.category, amount };
            }
            return obj;
        }

        public static decimal Sum(Object[] products)
        {
            decimal total = 0;
            foreach (var item in products)
            {
                total += Convert.ToDecimal((item as dynamic).amount);
            }
            return total;
        }

        public static decimal RoundUpForCharity(decimal amount)
        {
            return Math.Ceiling(amount);
        }

        public override string ToString() => "TODO:";
    }

    public class ChainableProduct
    {
        private Object[] _products = Array.Empty<Object>();

        public ChainableProduct GetProducts()
        {
            // Fetch products and load private data(backing) store
            _products = ServiceLayer.GetProducts();
            return this;
        }

        public ChainableProduct CategoryFilter(string filter)
        {
            var obj = Array.Empty<Object>();
            for (int i = 0; i < _products.Length; i++)
            {
                var item = (dynamic)_products[i];
                if (item.category == filter)
                {
                    Array.Resize(ref obj, obj.Length + 1);
                    obj[^1] = _products[i];
                }
            }
            _products = obj;
            return this;
        }

        public ChainableProduct AmountRangeFilter(decimal min, decimal max)
        {
            var obj = Array.Empty<Object>();
            for (int i = 0; i < _products.Length; i++)
            {
                var item = (dynamic)_products[i];
                if (item.amount >= min && item.amount <= max)
                {
                    Array.Resize(ref obj, obj.Length + 1);
                    obj[^1] = _products[i];
                }
            }
            _products = obj;
            return this;
        }

        public decimal Sum
        {
            get
            {
                decimal total = 0m;
                foreach (var item in _products)
                {
                    total += Convert.ToDecimal((item as dynamic).amount);
                }
                return total;
            }
        }

        public decimal RoundUpForCharity
        {
            get
            {
                return Math.Ceiling(this.Sum);
            }
        }
    }

    public class Category
    {
        public const string Food = "Food";
        public const string Clothing = "Clothing";
    }

    public static class DecimalExtention
    {
        public static decimal RoundUpForCharity(this decimal amount)
        {
            return Math.Ceiling(amount);
        }
    }

    public class ServiceLayer
    {
        public static Object[] GetProducts()
        {
            return new Object[]
            {
                new { name = "Product 1", amount = 1.25m, category = "Food" },
                new { name = "Product 2", amount = 55.23m, category = "Food" },
                new { name = "Product 3", amount = 78.99m, category = "Clothing" },
                new { name = "Product 4", amount = 9.45m, category = "Food" },
                new { name = "Product 5", amount = 24.98m, category = "Clothing" },
                new { name = "Product 6", amount = 62.12m, category = "Clothing" },
                new { name = "Product 7", amount = 2.69m, category = "Food" },
            };
        }
    }
}