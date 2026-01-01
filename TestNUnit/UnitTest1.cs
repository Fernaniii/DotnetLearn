namespace TestNUnit
{

    public enum ApprovalStatus
    {
        Pending,
        InProgress,
        Approved,
        Rejected
    }
    
    public class Bird()
    {
        public int NumberOfWing { get; set; }
    }

    [TestFixture]
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
            var bird = new Bird();
            bird.NumberOfWing = 2;
            
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TestCase(90)]
        //[TestCase(100)]// This test case will fail Comment to make it Passed
        public void Test2(int number)
        {
            // Assign the expected value
            var expected = 90;

            //Act

            // Assert
            Assert.That((expected), Is.EqualTo(number));
            
            //Assert.Pass();
        }

        [TestCase(ApprovalStatus.Pending)]
        public void Test3(ApprovalStatus status)
        {
            // Assign the expected value
            var expected = ApprovalStatus.Pending;
            // Can be Also use in enum 


            //Act

            // Assert
            Assert.That(expected, Is.EqualTo(status));
            
            //Assert.Pass();
        }


        // Testing with Class Property
        [TestCase(2)]
        public void Test4(int bird)
        {
            // Assign the expected value
            var chicken = new Bird();
            chicken.NumberOfWing = 2;

            //Act
            

            // Assert
            Assert.That(chicken.NumberOfWing, Is.EqualTo(bird));
            
            //Assert.Pass();
        }


    }
}
