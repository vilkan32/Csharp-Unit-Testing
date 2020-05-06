namespace INStock
{
    using INStock.Contracts;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class ProductStock : IProductStock
    {
        private List<IProduct> products;

        public ProductStock()
        {
            this.products = new List<IProduct>();
        }
        // done
        public void Add(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product cannot be null.");
            }

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].CompareTo(product) == 0)
                {
                    products[i].Quantity += product.Quantity;
                    products[i].Price = product.Price;
                    return;
                }
            }
            
            this.products.Add(product);       
        }

        // done
        public IProduct this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index.");
                }

                return this.products[index];
            }
            set
            {

                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index.");
                }

                if (value == null)
                {
                    throw new ArgumentNullException("Cannot set null product in stock.");
                }
               
                this.products[index] = value;
            }
        }
        // done
        public int Count
        {
            get => this.products.Count;
        }
        // done
        public bool Contains(IProduct product)
        {
            foreach (var prod in this.products)
            {
                if (prod.CompareTo(product) == 0)
                {
                    return true;
                }
            }

            return false;
        }
        // done
        public IProduct Find(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Invalid index.");
            }

            return this.products[index];
        }
        // done
        public IEnumerable<IProduct> FindAllByPrice(decimal price)
        {
            var result = this.products.Where(x => x.Price == price);

            if (result.Count() == 0)
            {
                return new List<Product>();
            }
            else
            {
                return result.ToList();
            }
        }
        // done
        public IEnumerable<IProduct> FindAllByQuantity(int quantity)
        {
            var result = this.products.Where(x => x.Quantity == quantity);

            if (result.Count() == 0)
            {
                return new List<Product>();
            }
            else
            {
                return result.ToList();
            }
        }
        // done
        public IEnumerable<IProduct> FindAllInRange(decimal lo, decimal hi)
        {
            var result = this.products.Where(x => x.Price >= lo && x.Price <= hi).OrderByDescending(x => x.Price);

            if (result.Count() == 0)
            {
                return new List<IProduct>();
            }

            return result.ToList();
        }
        // done
        public IProduct FindByLabel(string label)
        {
            var result = this.products.FirstOrDefault(x => x.Label == label);

            if (result == null)
            {
                throw new ArgumentException("Label cannot be found.");

            }
            else return result;
        }

        // done
        public IProduct FindMostExpensiveProduct()
        {
            if (this.products.Count == 0)
            {
                throw new InvalidOperationException("There are currently no products.");
            }
            var result = this.products.OrderByDescending(x => x.Price).First();

            return result;
        }

        // done
        public bool Remove(IProduct product)
        {
            for (int i = 0; i < this.products.Count; i++)
            {
                if (this.products[i].CompareTo(product) == 0)
                {
                    if (this.products[i].Quantity >= product.Quantity)
                    {
                        this.products[i].Quantity -= product.Quantity;
                        return true;
                    }
                }
            }

            return false;
        }
        // done
        public IEnumerator<IProduct> GetEnumerator()
        {
            foreach (var item in this.products)
            {
                yield return item;
            }
        }
        // done
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
