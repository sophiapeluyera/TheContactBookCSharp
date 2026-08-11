using Xunit;
using TheContactBook;
using System;

namespace TheContactBook.Tests
{
    public class ContactTests
    {
        // ---------- Constructor & Getters ----------

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            var c = new Contact("John", "Doe", "123", "john@example.com");

            Assert.Equal("John", c.GetFname());
            Assert.Equal("Doe", c.GetLname());
            Assert.Equal("123", c.GetPhone());
            Assert.Equal("john@example.com", c.GetEmail());
        }

        // ---------- Setters ----------

        [Fact]
        public void Setters_ShouldUpdateProperties()
        {
            var c = new Contact("A", "B", "C", "D");

            c.SetFname("John");
            c.SetLname("Doe");
            c.SetPhone("555-1234");
            c.SetEmail("john@example.com");

            Assert.Equal("John", c.GetFname());
            Assert.Equal("Doe", c.GetLname());
            Assert.Equal("555-1234", c.GetPhone());
            Assert.Equal("john@example.com", c.GetEmail());
        }

        // ---------- ToString ----------

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            var c = new Contact("John", "Doe", "123", "john@example.com");

            var expected = "Contact: John Doe, Phone: 123, Email: john@example.com";

            Assert.Equal(expected, c.ToString());
        }

        // ---------- Equals(Contact) ----------

        [Fact]
        public void Equals_ShouldReturnTrueForSameReference()
        {
            var c = new Contact("John", "Doe", "123", "john@example.com");

            Assert.True(c.Equals(c));
        }

        [Fact]
        public void Equals_ShouldReturnFalseForNull()
        {
            var c = new Contact("John", "Doe", "123", "john@example.com");

            Assert.False(c.Equals(null));
        }

        [Fact]
        public void Equals_ShouldReturnTrueForEqualValues()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.True(c1.Equals(c2));
        }

        [Fact]
        public void Equals_ShouldReturnFalseForDifferentValues()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("Jane", "Doe", "123", "john@example.com");

            Assert.False(c1.Equals(c2));
        }

        // ---------- Equals(object) ----------

        [Fact]
        public void EqualsObject_ShouldReturnFalseForDifferentType()
        {
            var c = new Contact("John", "Doe", "123", "john@example.com");

            Assert.False(c.Equals("not a contact"));
        }

        [Fact]
        public void EqualsObject_ShouldReturnTrueForEqualContact()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.True(c1.Equals((object)c2));
        }

        // ---------- Operator != ----------

        [Fact]
        public void OperatorNotEqual_ShouldReturnFalseForEqualContacts()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.False(c1 != c2);
        }

        [Fact]
        public void OperatorNotEqual_ShouldReturnTrueForDifferentContacts()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("Jane", "Doe", "123", "john@example.com");

            Assert.True(c1 != c2);
        }

        [Fact]
        public void OperatorNotEqual_ShouldHandleNullsCorrectly()
        {
            Contact? c1 = null;
            Contact? c2 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.True(c1 != c2);
            Assert.True(c2 != c1);
            Assert.False(c1 != null);
        }

        // ---------- GetHashCode ----------

        [Fact]
        public void GetHashCode_ShouldBeEqualForEqualContacts()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("John", "Doe", "123", "john@example.com");

            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldDifferForDifferentContacts()
        {
            var c1 = new Contact("John", "Doe", "123", "john@example.com");
            var c2 = new Contact("Jane", "Doe", "123", "john@example.com");

            Assert.NotEqual(c1.GetHashCode(), c2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeConsistentAcrossMultipleCalls()
        {
            var c = new Contact("John", "Doe", "123", "john@example.com");

            var h1 = c.GetHashCode();
            var h2 = c.GetHashCode();
            var h3 = c.GetHashCode();

            Assert.Equal(h1, h2);
            Assert.Equal(h2, h3);
        }
    }
}
