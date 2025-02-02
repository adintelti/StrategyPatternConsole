namespace StrategyPatternConsole
{
    internal class Program
    {
        public interface IDiscountStrategy
        {
            decimal CalculatePrice(decimal originalPrice);
        }

        public class NoDiscountStrategy : IDiscountStrategy
        {
            public decimal CalculatePrice(decimal originalPrice)
            {
                return originalPrice;
            }
        }

        public class PercentageDiscountStrategy : IDiscountStrategy
        {
            private readonly decimal _percentage;

            public PercentageDiscountStrategy(decimal percentage)
            {
                _percentage = percentage;
            }

            public decimal CalculatePrice(decimal originalPrice)
            {
                return originalPrice - (originalPrice * _percentage / 100);
            }
        }

        public class FixedDiscountStrategy : IDiscountStrategy
        {
            private readonly decimal _discountAmount;

            public FixedDiscountStrategy(decimal discountAmount)
            {
                _discountAmount = discountAmount;
            }

            public decimal CalculatePrice(decimal originalPrice)
            {
                return originalPrice - _discountAmount;
            }
        }

        // Classe Contexto
        public class Product
        {
            private readonly IDiscountStrategy _discountStrategy;
            public string Name { get; }
            public decimal Price { get; }

            public Product(string name, decimal price, IDiscountStrategy discountStrategy)
            {
                Name = name;
                Price = price;
                _discountStrategy = discountStrategy;
            }

            public decimal GetFinalPrice()
            {
                return _discountStrategy.CalculatePrice(Price);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao exemplo do padrão Strategy!\n");

            var product1 = new Product("Produto 1 - estratégia sem desconto", 100m, new NoDiscountStrategy());
            Console.WriteLine($"{product1.Name}: Preço final = {product1.GetFinalPrice()}\n");

            var product2 = new Product("Produto 2 - estratégia percentual", 200m, new PercentageDiscountStrategy(15));
            Console.WriteLine($"{product2.Name}: Preço final = {product2.GetFinalPrice()}\n");

            var product3 = new Product("Produto 3 - estratégia desconto valor fixo", 300m, new FixedDiscountStrategy(50));
            Console.WriteLine($"{product3.Name}: Preço final = {product3.GetFinalPrice()}\n");

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
