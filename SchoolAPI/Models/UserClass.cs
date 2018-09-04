using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class UserPublicInfoClass
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string fullName { get; set; }
        public byte[] profileImage { get; set; }

        public UserPublicInfoClass(USERS user)
        {
            setInstance(user);
        }

        public UserPublicInfoClass(int? userId)
        {
            if (userId == null)
                return;

            Entities e = new Entities();
            setInstance(e.USERS.FirstOrDefault(u => u.USER_ID == userId));

        }

        private void setInstance(USERS user)
        {
            if (user == null)
                return;

            id = user.USER_ID;
            userName = user.USER_NAME;
            fullName = user.FULL_NAME;
            profileImage = user.IMAGE;
        }
        
    }

    public class UserPrivateInfoClass : UserPublicInfoClass
    {
        private USERS user;
        public string password { get; set; }
        public short? needApprove { get; set; }
        public ParentClass parents { get; set; }
        public UserTypeClass type { get; set; }
        public List<GroupClass> groups { get; set; }
        public List<ClassClass> classes { get; set; }
        public List<SubjectClass> subjects { get; set; }
        public List<ChildClass> children { get; set; }

        public UserPrivateInfoClass(USERS user):base(user)
        {
            setInstance(user);
        }

        public UserPrivateInfoClass(int userId) : base(userId)
        {
            Entities e = new Entities();
            setInstance(e.USERS.FirstOrDefault(u => u.USER_ID == userId));
        }

        private void setInstance(USERS user)
        {
            if (user == null)
                return;

            this.user = user;

            password = user.USER_PASSWORD;
            needApprove = user.NEED_APPROVE;
            parents = GetParents();
            type = GetUserType();
            classes = GetClasses();
            groups = GetGroups();
            subjects = GetSubjects();
            children = GetChildren();
        }

        private UserTypeClass GetUserType()
        {
            int type = user.TYPE == null ? 0 : (int)user.TYPE;
            switch (type)
            {
                case 1: return new UserTypeClass() { id = user.ADMINS.Last().ADMIN_ID, userType = UserType.Admin };
                case 2: return new UserTypeClass() { id = user.TEACHERS.Last().TEACHER_ID, userType = UserType.Teacher };
                case 3: return new UserTypeClass() { id = user.STUDENTS.Last().STUDENT_ID, userType = UserType.Student };
                case 4: return new UserTypeClass() { id = user.PARENTS.Last().PARENT_ID, userType = UserType.Parent };
            }

            return null;
        }

        private List<ClassClass> GetClasses()
        {
            List<ClassClass> classes = new List<ClassClass>();



            if (user.TYPE == 2)
                foreach (var _subject in user.TEACHERS.Last().TEACHER_SUBJECTS)
                    classes.Add(new ClassClass(_subject.SUBJECTS.CLASSES));

            else if (user.TYPE == 3)
                foreach (var _class in user.STUDENTS)
                    classes.Add(new ClassClass(_class.CLASSES));

            return classes;
        }

        private List<GroupClass> GetGroups()
        {
            List<GroupClass> groups = new List<GroupClass>();

            foreach (var _group in user.GROUP_MEMBERS)
                groups.Add(new GroupClass(_group.GROUPS));


            return groups;
        }

        private List<SubjectClass> GetSubjects()
        {
            List<SubjectClass> subjects = new List<SubjectClass>();

            //if(user.ADMINS.Count > 0)
            //    foreach (var item in user.ADMINS.Last().S)
            //    {

            //    }

            if (user.TYPE == 2)
                foreach (var item in user.TEACHERS.Last().TEACHER_SUBJECTS)
                    subjects.Add(new SubjectClass(item.SUBJECTS));

            if (user.TYPE == 3)
                foreach (var item in user.STUDENTS.Last().STUDENT_SUBJECTS)
                    subjects.Add(new SubjectClass(item.SUBJECTS)); 
            return subjects;
        }

        private ParentClass GetParents()
        {
            if (user.TYPE == 3)
            {
                var parent = user.STUDENTS.Last().STUDENTS_PARENTS.Last().PARENTS;
                return new ParentClass(parent);
            }

            return null;
        }

        private List<ChildClass> GetChildren()
        {
            List<ChildClass> children = new List<ChildClass>();
            if (user.TYPE == 4)
                foreach (var item in user.PARENTS.Last().STUDENTS_PARENTS)
                    children.Add(new ChildClass(item.STUDENTS));

            return children;
        }
    }
}