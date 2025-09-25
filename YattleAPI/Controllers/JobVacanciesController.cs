using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using YattleAPI.CustomDecorator;

namespace YattleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CheckBearerToken]
    public class JobVacanciesController : ControllerBase
    {
        public class JobVacancy
        {
            public int Id { get; set; }
            public string JobTitle { get; set; } = string.Empty;
            public string CompanyName { get; set; } = string.Empty;
            public string JobDescription { get; set; } = string.Empty;
            public DateTime DateFirstAdvertised { get; set; }
            public DateTime ClosingDate { get; set; }
            public decimal AnnualRemunerationGBP { get; set; }
            public string CompanyBenefits { get; set; } = string.Empty;
        }

        // In-memory list of job vacancies
        private static List<JobVacancy> vacancies = new List<JobVacancy>
        {
            new JobVacancy {
                Id = 1,
                JobTitle = "Junior Software Developer",
                CompanyName = "TechNova Ltd",
                JobDescription = "Develop and maintain web applications using .NET.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-7),
                ClosingDate = DateTime.UtcNow.AddDays(23),
                AnnualRemunerationGBP = 28000,
                CompanyBenefits = "Health insurance, gym membership, flexible hours"
            },
            new JobVacancy {
                Id = 2,
                JobTitle = "Graduate Data Analyst",
                CompanyName = "Insight Analytics",
                JobDescription = "Analyze datasets and generate business insights.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-10),
                ClosingDate = DateTime.UtcNow.AddDays(20),
                AnnualRemunerationGBP = 26000,
                CompanyBenefits = "Remote work, pension plan, training budget"
            },
            new JobVacancy {
                Id = 3,
                JobTitle = "Marketing Assistant",
                CompanyName = "BrightSpark Marketing",
                JobDescription = "Support marketing campaigns and social media outreach.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-5),
                ClosingDate = DateTime.UtcNow.AddDays(25),
                AnnualRemunerationGBP = 24000,
                CompanyBenefits = "Travel allowance, team events, mentoring"
            },
            new JobVacancy {
                Id = 4,
                JobTitle = "Junior Accountant",
                CompanyName = "FinEdge Accountancy",
                JobDescription = "Assist with financial reporting and bookkeeping.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-3),
                ClosingDate = DateTime.UtcNow.AddDays(27),
                AnnualRemunerationGBP = 25000,
                CompanyBenefits = "Study support, private healthcare, bonus scheme"
            },
            new JobVacancy {
                Id = 5,
                JobTitle = "Graduate HR Coordinator",
                CompanyName = "PeopleFirst HR",
                JobDescription = "Coordinate recruitment and onboarding processes.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-8),
                ClosingDate = DateTime.UtcNow.AddDays(22),
                AnnualRemunerationGBP = 23000,
                CompanyBenefits = "Flexible working, wellness programs, paid volunteering"
            },
            new JobVacancy {
                Id = 6,
                JobTitle = "Junior Graphic Designer",
                CompanyName = "CreativeWorks Studio",
                JobDescription = "Design graphics for digital and print media.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-6),
                ClosingDate = DateTime.UtcNow.AddDays(24),
                AnnualRemunerationGBP = 22000,
                CompanyBenefits = "Creative workshops, equipment allowance, team lunches"
            },
            new JobVacancy {
                Id = 7,
                JobTitle = "Graduate Sales Executive",
                CompanyName = "SalesPro Solutions",
                JobDescription = "Generate leads and support sales operations.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-9),
                ClosingDate = DateTime.UtcNow.AddDays(21),
                AnnualRemunerationGBP = 27000,
                CompanyBenefits = "Commission, travel expenses, career progression"
            },
            new JobVacancy {
                Id = 8,
                JobTitle = "Junior IT Support Engineer",
                CompanyName = "NetSecure IT",
                JobDescription = "Provide technical support to clients and staff.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-4),
                ClosingDate = DateTime.UtcNow.AddDays(26),
                AnnualRemunerationGBP = 23000,
                CompanyBenefits = "Certifications, paid overtime, company laptop"
            },
            new JobVacancy {
                Id = 9,
                JobTitle = "Graduate Research Assistant",
                CompanyName = "Innovate Research Group",
                JobDescription = "Assist in research projects and data collection.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-2),
                ClosingDate = DateTime.UtcNow.AddDays(28),
                AnnualRemunerationGBP = 21000,
                CompanyBenefits = "Conference attendance, publication support, mentoring"
            },
            new JobVacancy {
                Id = 10,
                JobTitle = "Junior Business Analyst",
                CompanyName = "Visionary Consulting",
                JobDescription = "Support business analysis and process improvement.",
                DateFirstAdvertised = DateTime.UtcNow.AddDays(-1),
                ClosingDate = DateTime.UtcNow.AddDays(29),
                AnnualRemunerationGBP = 26000,
                CompanyBenefits = "Professional development, travel allowance, team socials"
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<JobVacancy>> GetJobVacancies()
        {
            return Ok(vacancies);
        }

        [HttpGet("{id}")]
        public ActionResult<JobVacancy> GetJobVacancy(int id)
        {
            var vacancy = vacancies.FirstOrDefault(v => v.Id == id);
            if (vacancy == null)
                return NotFound();
            return Ok(vacancy);
        }

        [HttpPost]
        public ActionResult<JobVacancy> CreateJobVacancy([FromBody] JobVacancy vacancy)
        {
            vacancy.Id = vacancies.Any() ? vacancies.Max(v => v.Id) + 1 : 1;
            vacancies.Add(vacancy);
            return CreatedAtAction(nameof(GetJobVacancy), new { id = vacancy.Id }, vacancy);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateJobVacancy(int id, [FromBody] JobVacancy updatedVacancy)
        {
            var vacancy = vacancies.FirstOrDefault(v => v.Id == id);
            if (vacancy == null)
                return NotFound();
            updatedVacancy.Id = id;
            int index = vacancies.FindIndex(v => v.Id == id);
            vacancies[index] = updatedVacancy;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJobVacancy(int id)
        {
            var vacancy = vacancies.FirstOrDefault(v => v.Id == id);
            if (vacancy == null)
                return NotFound();
            vacancies.Remove(vacancy);
            return NoContent();
        }
    }
}
