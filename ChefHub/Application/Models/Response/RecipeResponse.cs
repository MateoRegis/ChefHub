﻿namespace Application.Models.Response
{
    public class RecipeResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? Ingredients { get; set; }
        public string? Instructions { get; set; }
        public string? UrlImage { get; set; }
        public List<string>? Categories { get; set; }
        public List<CommentResponse> Comments { get; set; } = new List<CommentResponse>();
        public int? PreparationTime { get; set; }
        public string? Difficulty { get; set; }
        public UserResponse? UserResponse { get; set; }  
    }
}
