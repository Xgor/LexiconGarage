using LexiconGarage.Models;
using LexiconGarage.Vehicles;
using Moq;

namespace GarageTest;


public class GarageTests
{
    public Garage<Vehicle> GenerateFilledGarage()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(3);
        
        // Act
        garage.AddToEmpty(new Car("abc123",4,"Red",Fuel.Gasoline));
        garage.AddToEmpty(new Car("def456",4,"Blue",Fuel.Diesel));
        garage.AddToEmpty(new Airplane("1111-111",8,"White",4));
        
        return garage;
    }

    public Vehicle GetMockAirplane()
    {
        return new Airplane("123abc",8,"White",4);
    }
    
    [Fact]
    public void Garage_AddTo()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(1);
        Vehicle input = GetMockAirplane();
        int index = garage.AddToEmpty(input);
        
        // Act
        Vehicle result = garage.GetByIndex(index);

        // Assert
        Assert.Equal(input,result);
    }
    
    [Fact]
    public void Garage_GetCount()
    {
        // Arrange
        var garage = GenerateFilledGarage();
        
        // Assert
        Assert.Equal(3,garage.filledValueCount);
    }
    
    [Fact]
    public void Garage_GetByRegistrationNumber()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(1);
        Vehicle input = GetMockAirplane();
        int index = garage.AddToEmpty(input);
        
        // Act
        Vehicle? result = garage.GetByRegistrationNumber("123abc");

        // Assert
        Assert.Equal(input,result);
    } 
    
    [Fact]
    public void Garage_AddAndRemoveWithIndex()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(1);
        
        // Act
        Vehicle input = GetMockAirplane();
        int index = garage.AddToEmpty(input);
        Vehicle result = garage.RemoveAndGet(index);

        // Assert
        Assert.Equal(input,result);
        Assert.Equal(0, garage.filledValueCount);
    }
    
    [Fact]
    public void Garage_AddAndRemoveWithRegistrationNumber()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(1);
        
        // Act
        Vehicle input = GetMockAirplane();
        int index = garage.AddToEmpty(input);
        Vehicle result = garage.RemoveAndGet(index);

        // Assert
        Assert.Equal(input,result);
        Assert.Equal(0, garage.filledValueCount);
    }

    [Fact]
    public void Garage_KnowIfFull()
    {
        // Arrange
        Garage<Vehicle> garage = GenerateFilledGarage();
        
        // Assert
        Assert.True(garage.IsFull);
    }

    [Fact]
    public void Garage_KnowIfNotFull()
    {
        // Arrange
        Garage<Vehicle> garage = GenerateFilledGarage();
        
        // Act
        garage.Remove(0);
        
        // Assert
        Assert.False(garage.IsFull);
    }
    
    [Fact]
    public void Garage_GetEmptySlots()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(11);
        
        // Act
        garage.AddToEmpty(new Car("456AVC",4,"Black",Fuel.Gasoline));
        
        // Assert
        Assert.Equal(10,garage.EmptySlots);
    }
    
    [Fact]
    public void Garage_CheckIfEmpty()
    {
        // Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(11);
        
        // Act
        
        // Assert
        Assert.True(garage.IsEmpty);
    }
    
    

}