namespace FindMajorityElement.Tests.TestClasses
{
    /// <summary>
    /// Custom test class used primarily for comparison testing
    /// </summary>
    internal class Product
    {
        public string Name { get; }

        public Product(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Product product)
            {
                return Name == product.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
