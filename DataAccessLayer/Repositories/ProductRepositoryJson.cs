using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ProductManagementApp.DataAccessLayer.Interfaces;
using ProductManagementApp.Models;

namespace ProductManagementApp.DataAccessLayer.Repositories;

public class ProductRepositoryJson : IProductRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions;

    public ProductRepositoryJson()
    {
        string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        if (!Directory.Exists(dataFolder))
        {
            Directory.CreateDirectory(dataFolder);
        }

        _filePath = Path.Combine(dataFolder, "products.json");
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        if (!File.Exists(_filePath)) return new List<ProductModel>();

        string json = await File.ReadAllTextAsync(_filePath);
        if (string.IsNullOrWhiteSpace(json)) return new List<ProductModel>();

        try
        {
            return JsonSerializer.Deserialize<List<ProductModel>>(json, _jsonOptions) ?? new List<ProductModel>();
        }
        catch (JsonException)
        {
            throw new InvalidDataException("Data storage file contains invalid JSON format.");
        }
    }

    public async Task<ProductModel?> GetByIdAsync(Guid id)
    {
        var products = await GetAllAsync();
        return products.FirstOrDefault(p => p.Id == id);
    }

    public async Task AddAsync(ProductModel product)
    {
        var products = await GetAllAsync();
        products.Add(product);
        await SaveAllAsync(products);
    }

    public async Task UpdateAsync(ProductModel product)
    {
        var products = await GetAllAsync();
        int index = products.FindIndex(p => p.Id == product.Id);
        if (index != -1)
        {
            products[index] = product;
            await SaveAllAsync(products);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var products = await GetAllAsync();
        products.RemoveAll(p => p.Id == id);
        await SaveAllAsync(products);
    }

    public async Task<List<ProductModel>> SearchAsync(string keyword)
    {
        var products = await GetAllAsync();
        if (string.IsNullOrWhiteSpace(keyword)) return products;

        return products.Where(p =>
            p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            p.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

    private async Task SaveAllAsync(List<ProductModel> products)
    {
        string json = JsonSerializer.Serialize(products, _jsonOptions);
        await File.WriteAllTextAsync(_filePath, json);
    }
}