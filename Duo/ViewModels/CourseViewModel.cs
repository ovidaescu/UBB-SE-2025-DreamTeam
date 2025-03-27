﻿using Duo.Models;
using DuolingoNou.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DuolingoNou.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        private readonly ICourseService _courseService;
        private ObservableCollection<Course> _enrolledCourses;

        public ObservableCollection<Course> EnrolledCourses
        {
            get => _enrolledCourses;
            set
            {
                _enrolledCourses = value;
                OnPropertyChanged();
            }
        }

        public CourseViewModel(ICourseService courseService = null)
        {
            _courseService = courseService ?? new MockCourseService();
            EnrolledCourses = new ObservableCollection<Course>();
            LoadCoursesAsync();
        }

        private async Task LoadCoursesAsync()
        {
            var courses = await _courseService.GetEnrolledCoursesAsync();
            foreach (var course in courses)
            {
                EnrolledCourses.Add(course);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}