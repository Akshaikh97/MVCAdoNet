using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly SqlConnection _con;

        public CategoryController(SqlConnection con)
        {
            _con = con;
        }

        // GET: /Category/Index
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var categories = new List<Category>();
            try
            {
                _con.Open();
                var command = new SqlCommand("GetCategories", _con);
                command.CommandType = CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        Id = (int)reader["Id"],
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
                reader.Close();
            }
            finally
            {
                _con.Close();
            }

            return View(categories);
        }

        // GET: /Category/Details/5
        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            Category category = null;
            try
            {
                _con.Open();
                var command = new SqlCommand("GetCategoryById", _con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    category = new Category
                    {
                        Id = (int)reader["Id"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                }
                reader.Close();
            }
            finally
            {
                _con.Close();
            }

            return category == null ? NotFound() : View(category);
        }

        // GET: /Category/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _con.Open();
                    var command = new SqlCommand("InsertCategory", _con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    command.ExecuteNonQuery();
                }
                finally
                {
                    _con.Close();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: /Category/Edit/5
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            Category category = null;
            try
            {
                _con.Open();
                var command = new SqlCommand("GetCategoryById", _con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    category = new Category
                    {
                        Id = (int)reader["Id"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                }
                reader.Close();
            }
            finally
            {
                _con.Close();
            }

            return category == null ? NotFound() : View(category);
        }

        // POST: /Category/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _con.Open();
                var command = new SqlCommand("UpdateCategory", _con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", category.Id);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                command.ExecuteNonQuery();
            }
            finally
            {
                _con.Close();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Category/Delete/5
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Category category = null;
            try
            {
                _con.Open();
                var command = new SqlCommand("GetCategoryById", _con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    category = new Category
                    {
                        Id = (int)reader["Id"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                }
                reader.Close();
            }
            finally
            {
                _con.Close();
            }

            return category == null ? NotFound() : View(category);
        }

        // POST: /Category/Delete/{id}
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _con.Open();
                var command = new SqlCommand("DeleteCategory", _con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            finally
            {
                _con.Close();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
