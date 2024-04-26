using System.Collections;

namespace TODOList.TodoCore.Models.ViewModels
{
    public class TodoViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public TodoViewModel()
        {
            
        }
        
    }
}
