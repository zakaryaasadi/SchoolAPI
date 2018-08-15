using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Views.ModelView
{
    public class UserView
    {
        public static UserClass Info(Entities e, USERS _user)
        {
            return new UserClass()
            {
                id = _user.USER_ID,
                userName = _user.USER_NAME,
                fullName = _user.FULL_NAME,
                password = _user.USER_PASSWORD,
                profileImage = _user.IMAGE,
                parents = GetParents(_user),
                type = GetUserType(_user),
                classes = GetClasses(_user),
                groups = GetGroups(_user),
                subjects = GetSubjects(_user),
                children = GetChildren(_user)
            };
        }


        private static UserTypeClass GetUserType(USERS user)
        {
            List<UserTypeClass> types = new List<UserTypeClass>();
            if (user.ADMINS.Count > 0)
                return new UserTypeClass() { id = user.ADMINS.Last().ADMIN_ID, userType = UserType.Admin };

            if (user.TEACHERS.Count > 0)
                return new UserTypeClass() { id = user.TEACHERS.Last().TEACHER_ID, userType = UserType.Teacher };

            if (user.PARENTS.Count > 0)
                return new UserTypeClass() { id =user.PARENTS.Last().PARENT_ID, userType = UserType.Parent };

            if (user.STUDENTS.Count > 0 )
                return new UserTypeClass() { id = user.STUDENTS.Last().STUDENT_ID, userType = UserType.Student };

            return null;
        }

        private static List<ClassClass> GetClasses(USERS user)
        {
            List<ClassClass> classes = new List<ClassClass>();

            foreach (var _class in user.STUDENTS)
                classes.Add(new ClassClass()
                {
                    id = _class.CLASSES.CLASS_ID,
                    name = _class.CLASSES.CLASS_NAME
                });

            if(user.TEACHERS.Count > 0)
                foreach (var _subject in user.TEACHERS.Last().TEACHER_SUBJECTS)
                    classes.Add(new ClassClass()
                    {
                        id = _subject.SUBJECTS.CLASSES.CLASS_ID,
                        name = _subject.SUBJECTS.CLASSES.CLASS_NAME
                    });

            return classes;
        }

        private static List<GroupClass> GetGroups(USERS user)
        {
            List<GroupClass> groups = new List<GroupClass>();

            foreach (var _group in user.GROUP_MEMBERS)
                groups.Add(new GroupClass()
                {
                    id = _group.GROUPS.GROUP_ID,
                    name = _group.GROUPS.NAME
                });


            return groups;
        }

        private static List<SubjectClass> GetSubjects(USERS user)
        {
            List<SubjectClass> subjects = new List<SubjectClass>();

            //if(user.ADMINS.Count > 0)
            //    foreach (var item in user.ADMINS.Last().S)
            //    {

            //    }

            if (user.TEACHERS.Count > 0)
                foreach (var item in user.TEACHERS.Last().TEACHER_SUBJECTS)
                    subjects.Add(new SubjectClass() { id = item.SUBJECTS.SUBJECT_ID, name = item.SUBJECTS.SUBJECT_NAME });

            if (user.STUDENTS.Count > 0)
                foreach (var item in user.STUDENTS.Last().STUDENT_SUBJECTS)
                    subjects.Add(new SubjectClass() { id = item.SUBJECTS.SUBJECT_ID, name = item.SUBJECTS.SUBJECT_NAME });
            return subjects;
        }

        private static ParentClass GetParents(USERS user)
        {
            if (user.STUDENTS.Count > 0) {
                var parent = user.STUDENTS.Last().STUDENTS_PARENTS.Last().PARENTS;
                if(parent != null)
                    return new ParentClass() { id = parent.PARENT_ID, father = parent.PARENT_FATHER_NAME, mother = parent.PARENT_MOTHER};
            }

            return null;
        }

        private static List<ChildClass> GetChildren(USERS user)
        {
            List<ChildClass> children = new List<ChildClass>();
            if(user.PARENTS.Count > 0)
                foreach (var item in user.PARENTS.Last().STUDENTS_PARENTS)
                    children.Add(new ChildClass() { id = item.STUDENTS.USERS.USER_ID, name = item.STUDENTS.STUDENT_NAME });

            return children;
        }
    }
}