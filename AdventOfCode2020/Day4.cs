using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day4
    {
        public static string _input = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";

        public static int Run(string input)
        {
            var valid = 0;
            var passports = InputUtils.SplitInputByBlankLines(input);
            foreach (var passport in passports)
            {
                var passportDict = new Dictionary<string, string>();
                var tokens = passport.Split(new char[] { '\r', '\n', ':', ' ' });
                var validTokens = tokens.Where(x => x.Length > 0).ToList();
                for (int i = 0; i < validTokens.Count(); i += 2)
                {
                    passportDict.Add(validTokens[i], validTokens[i + 1]);
                }
                if (passportDict.ContainsKey("ecl") && passportDict.ContainsKey("pid") && passportDict.ContainsKey("eyr") && passportDict.ContainsKey("hcl") && passportDict.ContainsKey("byr")
                    && passportDict.ContainsKey("iyr") && passportDict.ContainsKey("hgt"))
                {
                    if (ValidateFields(passportDict))
                    {
                        valid++;
                    }
                }
            }
            return valid;
        }

        public static bool ValidateFields(Dictionary<string, string> passport)
        {
            var valid = true;
            try
            {
                var byr = Int32.Parse(passport["byr"]);
                if (byr < 1920 || byr > 2002)
                {
                    return false;
                }
                var iyr = Int32.Parse(passport["iyr"]);
                if (iyr < 2010 || iyr > 2020)
                {
                    return false;
                }
                var eyr = Int32.Parse(passport["eyr"]);
                if (eyr < 2020 || eyr > 2030)
                {
                    return false;
                }
                var hgt = passport["hgt"];
                var hgtInt = Int32.Parse(hgt.Substring(0, hgt.Length - 2));
                if (hgt.Contains("cm"))
                {
                    if (hgtInt < 150 || hgtInt > 193)
                    {
                        return false;
                    }
                }
                else if (hgt.Contains("in"))
                {
                    if (hgtInt < 59 || hgtInt > 76)
                    {
                        return false;
                    }
                }
                var hcl = passport["hcl"];
                var hclPattern = "^[#][0-9a-f]{6}$";
                if (!RegexUtils.IsMatch(hclPattern, hcl))
                {
                        return false;
                }
                var ecl = passport["ecl"];
                if (!RegexUtils.IsMatch("^amb|blu|brn|gry|grn|hzl|oth$", ecl)) 
                { 
                    return false;
                }
                var pid = passport["pid"];
                var pidPattern = "^[0-9]{9}$";
                if (!RegexUtils.IsMatch(pidPattern, pid))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return valid;
        }
    }
}
