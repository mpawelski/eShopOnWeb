using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.UnitTests.Builders;
using System.Collections.Generic;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.OrderTests
{
    public class OrderTotal
    {
        private decimal _testUnitPrice = 42m;



        [Fact]
        public void IsNoDiscountWhereLessThanFiveItems()
        {
            var builder = new OrderBuilder();
            var items = new List<OrderItem>
            {
                new OrderItem(builder.TestCatalogItemOrdered, _testUnitPrice, 1)
            };

            var discount = new DiscountForOrder { PercentDiscountValue = 0.5m };

            var order = new OrderBuilder().WithItemsAndDiscount(items, discount);
            Assert.Equal(_testUnitPrice, order.Total());
        }

        [Fact]
        public void IsDiscountWhereAtLeastFiveItems()
        {
            var builder = new OrderBuilder();
            var items = new List<OrderItem>
            {
                new OrderItem(builder.TestCatalogItemOrdered, _testUnitPrice, 5)
            };
            var expectedTotalOrderPrice = 105; 

            var discount = new DiscountForOrder { PercentDiscountValue = 0.5m };

            var order = new OrderBuilder().WithItemsAndDiscount(items, discount);
            Assert.Equal(expectedTotalOrderPrice, order.Total());
        }

        [Fact]
        public void IsZeroForNewOrder()
        {
            var order = new OrderBuilder().WithNoItems();

            Assert.Equal(0, order.Total());
        }

        [Fact]
        public void IsCorrectGiven1Item()
        {
            var builder = new OrderBuilder();
            var items = new List<OrderItem>
            {
                new OrderItem(builder.TestCatalogItemOrdered, _testUnitPrice, 1)
            };
            var order = new OrderBuilder().WithItems(items);
            Assert.Equal(_testUnitPrice, order.Total());
        }

        [Fact]
        public void IsCorrectGiven3Items()
        {
            var builder = new OrderBuilder();
            var order = builder.WithDefaultValues();

            Assert.Equal(builder.TestUnitPrice * builder.TestUnits, order.Total());
        }
    }
}
