using Xunit;
using TheContactBook;

namespace TheContactBook.Tests
{
    public class ContactTests
    {
        // -------------------------------------------------------------
        // Constructor & Getters
        // -------------------------------------------------------------
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            var contact = new Contact("John", "Doe", "1234567890", "john@example.com");

            Assert.Equal("John", contact.GetFname());
            Assert.Equal("Doe", contact.GetLname());
            Assert.Equal("1234567890", contact.GetPhone());
            Assert.Equal("john@example.com", contact.GetEmail());
        }

        // -------------------------------------------------------------
        // Setters
        // -------------------------------------------------------------
        [Fact]
        public void Setters_ShouldUpdateValues()
        {
            var contact = new Contact("John", "Doe", "123", "john@example.com");

            contact.SetFname("Jane");
            contact.SetLname("Smith");
            contact.SetPhone("9876543210");
            contact.SetEmail("jane@example.com");

            Assert.Equal("Jane", contact.GetFname());
            Assert.Equal("Smith", contact.GetLname());
            Assert.Equal("9876543210", contact.GetPhone());
            Assert.Equal("jane@example.com", contact.GetEmail());
        }

        // -------------------------------------------------------------
        // ToString
        // -------------------------------------------------------------
        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            var contact = new Contact("John", "Doe", "1234567890", "john@example.com");

            var expected = "Contact: John Doe, Phone: 1234567890, Email: john@example.com";

            Assert.Equal(expected, contact.ToString());
        }

        // -------------------------------------------------------------
        // Equality
        // -------------------------------------------------------------
        [Fact]
        public void Equals_ShouldReturnTrueForSameValues()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.True(c1.Equals(c2));
            Assert.True(c1.Equals((object)c2));
        }

        [Fact]
        public void Equals_ShouldReturnFalseForDifferentValues()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("Jane", "Doe", "123", "john@example.com");

            Assert.False(c1.Equals(c2));
        }

        [Fact]
        public void Equals_ShouldReturnFalseForNull()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.False(c1.Equals(null));
        }

        [Fact]
        public void Equals_ShouldReturnTrueForSameReference()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.True(c1.Equals(c1));
        }

        // -------------------------------------------------------------
        // Operator !=
        // -------------------------------------------------------------
        [Fact]
        public void InequalityOperator_ShouldReturnTrueForDifferentContacts()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("Jane", "Doe", "123", "john@example.com");

            Assert.True(c1 != c2);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnFalseForEqualContacts()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.False(c1 != c2);
        }

        // -------------------------------------------------------------
        // GetHashCode
        // -------------------------------------------------------------
        [Fact]
        public void GetHashCode_ShouldBeEqualForEqualObjects()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldDifferForDifferentObjects()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("Jane", "Doe", "123", "john@example.com");

            Assert.NotEqual(c1.GetHashCode(), c2.GetHashCode());
        }
    }
}
