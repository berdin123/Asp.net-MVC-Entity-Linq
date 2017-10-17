using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseOdevim
{
    class Program
    {
        static void Main(string[] args)
        {
            // Select / Where / Join / Order By / Like
            DatabaseOdevimEntities odevimContext = new DatabaseOdevimEntities();

            //Kendi başına yazılan linq sorguları
            //Join ifadesini burada kullanamıyoruz. Kendi başına tek sorgu yaparken bunları kullanıyoruz..
            var CalisanList = odevimContext.EMPLOYEE.ToList(); // select * from
            var CalisanWhereList = odevimContext.EMPLOYEE.Where(Calisan => Calisan.Sex.Equals("M")).ToList();
            var CalisanWhereList_2 = odevimContext.EMPLOYEE.Where(Calisan => Calisan.Fname.StartsWith("a")).OrderByDescending(p => p.Salary).ToList();
            var CalisanWhereList_3 = odevimContext.EMPLOYEE.Select(Calisan => Calisan.Ssn).ToList();

            // Sanki bir nesneymiş gibi verilerle uğraşabiliriz...
            for (int i = 0; i < CalisanWhereList.Count; i++)
            {
                Console.WriteLine("Adi:" + CalisanWhereList[i].Fname);
                Console.WriteLine("Soyadi:" + CalisanWhereList[i].Lname);
                Console.WriteLine("Maasi: " + CalisanWhereList[i].Salary);

                Console.WriteLine();
            }
            //Sql e benzeyen linq sorguları
            // Join kurma işlemi sadece sql'e benzeye linq sorguları ile yapılıyor..
            var sCalisanList = (from Calisan in odevimContext.EMPLOYEE select Calisan).ToList();
            var sCalisanWhereList = (from Calisan in odevimContext.EMPLOYEE where Calisan.Sex.Equals("F") select Calisan);
            var sCalisanJoinList = (from Calisan in odevimContext.EMPLOYEE
                                    join Departman in odevimContext.DEPARTMENT on Calisan.Dno equals Departman.Dnumber
                                    where Departman.Dname.Contains("Research") //Like ifadesiyle aynı işlevi görür. İçerisinden belirtilen stringleri içerenleri alır.
                                    //Startswith ile o harfle başlayan harfleri alabiliriz. Endswith ile o harfle bitenleri getirtebiliriz.
                                    orderby Calisan.Salary descending //Maaşları azalan sırada listelemek için kullanılmıştır.
                                    select new
                                    {
                                        Calisan.Fname,
                                        Calisan.Lname,
                                        Calisan.Salary,
                                        Departman.Dname,
                                        Departman.Mgr_ssn,
                                        Departman.Mgr_start_date

                                    }).ToList();

            for (int i = 0; i < sCalisanJoinList.Count; i++)
            {
                Console.WriteLine("Adi:" + sCalisanJoinList[i].Fname);
                Console.WriteLine("Soyadi:" + sCalisanJoinList[i].Lname);
                Console.WriteLine("Maasi: " + sCalisanJoinList[i].Salary);
                Console.WriteLine("Departman Adi: " + sCalisanJoinList[i].Dname);
                Console.WriteLine("Mgr_ssn: " + sCalisanJoinList[i].Mgr_ssn);
                Console.WriteLine("Mgr_start_date: " + sCalisanJoinList[i].Mgr_start_date);

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
