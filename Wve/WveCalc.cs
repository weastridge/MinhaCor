using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wve
{
    /// <summary>
    /// some calculation methods
    /// </summary>
    public static class WveCalc
    {
        //calcculations......
        /// <summary>
        /// calculate body mass index
        /// </summary>
        /// <param name="wtKg">wt in kg</param>
        /// <param name="htM">ht in meters</param>
        /// <returns></returns>
        public static double CalculateBMI(double wtKg, double htM)
        {
            // bmi= kg/m2
            //return wtKg / (Math.Pow(htM, 2));
            return wtKg / (htM * htM);
        }

        /// <summary>
        /// calculate bmi and report details in text
        /// (age and gender required for details text)
        /// </summary>
        /// <param name="wtKg"></param>
        /// <param name="htM"></param>
        /// <param name="age">in years</param>
        /// <param name="isFemale"></param>
        /// <param name="flag">L,H,HH or empty string</param>
        /// <param name="text">narrative description of result</param>
        /// <returns></returns>
        public static double CalculateBMI(double wtKg,
            double htM,
            double age,
            bool isFemale,
            out string flag,
            out string text)
        {
            double bmi = double.MinValue; //the result
            double ibw = double.MinValue; //ideal body wt
            double lowNormal = double.MinValue;
            double highNormal = double.MinValue;
            double obese = double.MinValue;
            string scale = string.Empty; ;
            if ((age >= 18) && (age < 65))
            {
                scale = "_______<______>____>>_________";
                lowNormal = 18.5;
                highNormal = 25;
                obese = 30;
            }
            else if (age >= 65)
            {
                scale = "____________<______>____>>____";
                lowNormal = 23;
                highNormal = 30;
                obese = 35;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("Body Mass Index: ");
            //bmi= Wt(kg)/(Ht(m) ^2)
            bmi = wtKg / (htM * htM);
            flag = string.Empty;
            if (bmi < lowNormal)
                flag = "L";
            if (bmi > highNormal)
                flag = "H";
            if (bmi > obese)
                flag = "HH";
            sb.Append(string.Format("{0:#.00}", bmi));
            sb.Append(" ");
            sb.Append(flag);
            sb.Append(Environment.NewLine);
            //show references if age > 18
            if (age >= 18)            
            {
                int bmiInt = (int)Math.Floor(bmi);
                if (bmiInt < 11)
                {
                    sb.Append("*-");
                    sb.Append(scale);
                }
                else if (bmiInt > 40)
                {
                    sb.Append(scale);
                    sb.Append("+*");
                }
                else
                {
                    sb.Append(scale.Substring(0, bmiInt - 11));
                    sb.Append("*");
                    sb.Append(scale.Substring(bmiInt - 10, 30-bmiInt + 10));
                }
                sb.Append(Environment.NewLine);
                //one version of ideal body wt
                ibw = WveCalc.CalculateIBW(htM, isFemale);
                sb.Append("Approx ideal body wt is ");
                sb.Append(string.Format("{0:#.#}", ibw));
                sb.Append(" kg (");
                sb.Append(string.Format("{0:#.#}",
                    WveConvert.KilogramsToPounds(ibw)));
                sb.Append(" lbs.)");
                sb.Append(Environment.NewLine);
                //wt for bmi highnormal
                double topNlWt = highNormal * htM * htM;
                sb.Append("Top normal wt: ");
                sb.Append(string.Format("{0:#.#}", topNlWt));
                sb.Append(" kg (");
                sb.Append(string.Format("{0:#.#}",
                    WveConvert.KilogramsToPounds(topNlWt)));
                sb.Append(" lbs.) for bmi of ");
                sb.Append(highNormal.ToString());
                sb.Append(Environment.NewLine);
                //obese
                double obeseWt = obese * htM * htM;
                sb.Append("Obese wt: ");
                sb.Append(string.Format("{0:#.#}", obeseWt));
                sb.Append(" kg (");
                sb.Append(string.Format("{0:#.#}",
                    WveConvert.KilogramsToPounds(obeseWt)));
                sb.Append(" lbs.) for bmi of ");
                sb.Append(obese.ToString());
                sb.Append(Environment.NewLine);
            }//from if age >=18
            //surface area 
            sb.Append("Body Surface Area: ");
            sb.Append(string.Format("{0:#.##}", WveCalc.CalculateBSA(wtKg, htM)));
            sb.Append(" m^2");
            sb.Append(Environment.NewLine);
            text = sb.ToString();
            return bmi;
        }

        /// <summary>
        /// one calculation of ideal body wt in KG, using 50 kg + 2.3 per inch males, 
        /// 45.5 kg + 2.3 per inch over 5 ft
        /// </summary>
        /// <param name="htM"></param>
        /// <param name="isFemale"></param>
        /// <returns></returns>
        public static double CalculateIBW(double htM, bool isFemale)
        {
            double result;
            double htIn = WveConvert.CmToInches(htM * 100);
            if (isFemale)
                result = 45.5;
            else
                result = 50;
            result = result + ((htIn - 60) * 2.3);
            return result;
        }

        /// <summary>
        /// calculate body surface area
        /// </summary>
        /// <param name="wtKg">wt in kg</param>
        /// <param name="htM">ht in meters</param>
        /// <returns></returns>
        public static double CalculateBSA(double wtKg, double htM)
        {
            //bsa = .20247 * Ht (m) to .725 power  * wt (kg) to .425 power
            // per www.intmed.mcw.edu/clincalc/body.html
            return .20247 * (Math.Pow(htM, .725)) * (Math.Pow(wtKg, .425));
        }


        /*
         * 
         * 
Black Female #0.7 GFR = 166 x (SCr /0.7)-0.329 x (0.993)Age
Black Female 0.7 GFR = 166 x (SCr /0.7)-1.209 x (0.993)Age
Black Male #0.9 GFR = 163 x (SCr /0.9)-0.411 x (0.993)Age
Black Male 0.9 GFR = 163 x (SCr /0.9)-1.209 x (0.993)Age
White or other Female #0.7 GFR = 144 x (SCr /0.7)-0.329 x (0.993)Age
White or other Female 0.7 GFR = 144 x (SCr /0.7)-1.209 x (0.993)Age
White or other Male #0.9 GFR = 141 x (SCr /0.9)-0.411 x (0.993)Age
White or other Male 0.9 GFR = 141 x (SCr /0.9)-1.209 x (0.993)Age
CKD-EPI equation expressed as a single equation: GFR = 141 x min(SCr /k, 1)a x max(SCr /k, 1)-1.209 x 0.993Age x 1.018 [if female] x 1.159 [if black] where
SCr is standardized serum creatinine in mg/dL, k is 0.7 for females and 0.9 for males, a is -0.329 for females and -0.411 for males, min indicates the
minimum of SCr /k or 1, and max indicates the maximum of SCr /k or 1.
*Reprinted with permission from the American Society of Nephrology via the Copyright Clearance Center. Stevens LA, Levey AS.
         * 
         */

        /// <summary>
        /// returns calculated Glomerular Filtration Rate or
        /// double.MinValue if can't calculate; uses MDRD for adults > 18, 
        /// and Schwartz for peds
        /// </summary>
        /// <param name="creatinine">serum creatinine in mg/DL</param>
        /// <param name="age">age in years</param>
        /// <param name="lengthCm">optional for adults >= 18; use double.MinValue for null</param>
        /// <param name="weightKg">optional; use double.MinValue for null</param>
        /// <param name="isFemale">set true if female</param>
        /// <param name="isBlack">set true if African or African-American</param>
        /// <param name="isIDMS">set true if creatinine is referenced to 
        /// Isotope Dilution Mass Spectrometry which all modern values should be - default to true</param>
        /// <param name="result">outputs result with message qualifying the result if any</param>
        /// <param name="formula">outputs the formula used in the calculation</param>
        /// <returns></returns>
        public static double CalculateGFR(double creatinine,
            double age,
            double lengthCm,
            double weightKg,
            bool isFemale,
            bool isBlack,
            bool isIDMS,
            out string result,
            out string formula, 
            out string comment)
        {
            double result_CKDEpi = double.MinValue;
            double result_MDRD = double.MinValue;
            StringBuilder sbResult = new StringBuilder();
            StringBuilder sbFormula = new StringBuilder();
            StringBuilder sbComment = new StringBuilder();

            //balk for bad values
            if ((creatinine < 0) || (creatinine > 6))
            {
                sbResult.Append("Creatinine of ");
                sbResult.Append(creatinine.ToString());
                sbResult.Append(" mg/dL is outside calculable range.");
            }

            //for adults
            else if ((age >= 18) && (age < 120))
            {
                //now (written in 2012) we prefer CKD-Epi rather than MDRD!
                double k = 0.7; //if female
                double a = -0.329; //if female
                if(!isFemale)
                {
                    k = 0.9;
                    a=-0.411;
                }
                double min = 1;
                if(creatinine/k < 1)
                {
                    min = creatinine/k;
                }
                double max = 1;
                if(creatinine/k >1)
                {
                    max = creatinine/k;
                }
                result_CKDEpi = 141 * 
                    Math.Pow(min, a) * 
                    Math.Pow(max, -1.209) * 
                    Math.Pow(0.993, age);
                if (isFemale)
                {
                    result_CKDEpi = result_CKDEpi * 1.018;
                }
                if (isBlack)
                {
                    result_CKDEpi = result_CKDEpi * 1.159;
                }
                sbResult.Append("Adult estimated GFR by CKD-Epi method is ");
                sbResult.Append(Environment.NewLine);
                sbResult.Append("<<<< ");
                sbResult.Append(string.Format("{0:#}", result_CKDEpi));
                sbResult.Append(" ml/min/1.73 m^2 >>>>");
                sbResult.Append(Environment.NewLine);
                sbResult.Append("This is useful for estimating kidney health.");
                sbResult.Append(Environment.NewLine);

                sbFormula.Append("CKD-Epi:  ");
                sbFormula.Append(Environment.NewLine);
                sbFormula.Append("141 x min(sCr/k,1)^a x max(sCr/k,1)^-1.209 ");
                sbFormula.Append("x 0.993^Age ");
                sbFormula.Append("x [1.018 if female] x [1.159 if black]");
                sbFormula.Append(Environment.NewLine);
                sbFormula.Append("Where k=0.7 females, 0.9 males and a=-0.329 females, -0.411 males.");
                sbFormula.Append(Environment.NewLine);
                sbFormula.Append(Environment.NewLine);
                //new IDMS corrected MDRD
                if (isIDMS)
                {
                    sbFormula.Append("Alternative adult MDRD formula for standardized IDMS creat: ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append("GFR= 175 ");
                    sbFormula.Append("x creat(mg/dL) exp(-1.154) ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append("x age exp(-.203)");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append("x 1.212 if black ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append("x  .742 if female ");
                    sbFormula.Append(Environment.NewLine);
                    result_MDRD = 175 * (Math.Pow(creatinine, (-1.154))) * (Math.Pow(age, (-.203)));
                    if (isBlack)
                        result_MDRD = result_MDRD * 1.212;
                    if(isFemale)
                        result_MDRD = result_MDRD * .742;
                    sbResult.Append(Environment.NewLine);
                    sbResult.Append("For comparison the MDRD estimated GFR is: ");
                    sbResult.Append(string.Format("{0:#}", result_MDRD));
                    sbResult.Append(" ml/min/1.73 m^2");
                }
                else
                {
                    //no longer used....
                    sbFormula.Append("Adult MDRD formula for non-IDMS-standardized creat: ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append("GFR= 186.3 ");
                    sbFormula.Append("x creat(mg/dL) exp(-1.154) ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append("x age exp(-.203)");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append("x 1.212 if black ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append("x  .742 if female ");
                    sbFormula.Append(Environment.NewLine);
                    result_MDRD = 186.3 * (Math.Pow(creatinine, (-1.154))) * (Math.Pow(age, (-.203)));
                    if (isBlack)
                        result_MDRD = result_MDRD * 1.212;
                    if (isFemale)
                        result_MDRD = result_MDRD * .742;
                    sbResult.Append(Environment.NewLine);
                    sbResult.Append("For comparison the MDRD estimated GFR (non standard creat) is: ");
                    sbResult.Append(string.Format("{0:#}", result_MDRD));
                    sbResult.Append(" ml/min/1.73 m^2");
                }
                //add comment for adults
                sbComment.Append("MDRD estimated GFR of ");
                sbComment.Append(string.Format("{0:#}", result_MDRD));
                sbComment.Append(" is for an average sized person with given age and creatinine.");
                sbComment.Append(Environment.NewLine);
                sbComment.Append("This is useful for estimating degree of kidney function ");
                sbComment.Append("for average sized people but may need ");
                sbComment.Append("adjustment if not.");
                //if wt and ht are given...
                if ((weightKg > 30) &&
                    (weightKg < 200) &&
                    (lengthCm > 100) &&
                    (lengthCm < 300))
                {
                    double bsa = CalculateBSA(weightKg, lengthCm / 100);
                    double eGFRCorrected = result_MDRD *
                        bsa / 1.73;
                    sbComment.Append("The estimated GFR for given height and weight is ");
                    sbComment.Append(string.Format("{0:#}",result_MDRD));
                    sbComment.Append("*");
                    sbComment.Append(string.Format("{0:#.##}", bsa));
                    sbComment.Append("/1.73 = ");
                    sbComment.Append(string.Format("{0:#}", eGFRCorrected));
                    sbComment.Append(".  ");
                    sbComment.Append("\r\nThis estimates the actual GFR for this person in ml/min,");
                    sbComment.Append("but is not necessarily as good estimate of kidney health as ml/min/1.73m2.");
                }
                else
                {
                    //ht and wt not given
                    sbComment.Append("To correct for body size multiply by ");
                    sbComment.Append("Body Surface Area divided by 1.73 m^2.");
                }
                //add Cockcroft-Gault if wt given
                if ((weightKg > 30) &&
                    (weightKg < 200))
                {
                    double cG = (140 - age) * weightKg / (72 * creatinine);
                    if (isFemale)
                    {
                        cG = cG * .85;
                    }
                    sbComment.Append(Environment.NewLine);
                    sbComment.Append(Environment.NewLine);
                    sbComment.Append("For comparison, the Cockcroft-Gault estimate for given ");
                    sbComment.Append(" age, weight and creatinine is <<<<");
                    sbComment.Append(string.Format("{0:#}", cG));
                    sbComment.Append(" ml/min. >>>>");
                    sbComment.Append("\r\nThat is, [(140-age) * wt / (72*creat)], multiplied by ");
                    sbComment.Append(".85 if female.");

                    sbResult.Append(Environment.NewLine);
                    sbResult.Append(Environment.NewLine);
                    sbResult.Append("For comparison, the Cockcroft-Gault estimate for given ");
                    sbResult.Append(" age, weight and creatinine is <<<<");
                    sbResult.Append(string.Format("{0:#}", cG));
                    sbResult.Append(" ml/min. >>>>");
                    sbResult.Append(Environment.NewLine);
                    sbResult.Append("This is useful for dosing drugs.");


                    //if height is given.... add ideal body wt CG
                    if ((lengthCm > 100) &&
                            (lengthCm < 300))
                    {
                        double ibw = CalculateIBW(lengthCm / 100, isFemale);
                        double cGIbw = (140 - age) * ibw / (72 * creatinine);
                        if (isFemale)
                        {
                            cGIbw = cGIbw * .85;
                        }
                        sbComment.Append("\r\nIf we used the ideal body weight of ");
                        sbComment.Append(string.Format("{0:#}",
                            ibw));
                        sbComment.Append(" kg., the adjusted Cockcroft-Gault estimate would be ");
                        sbComment.Append(string.Format("{0:#}", cGIbw));
                        sbComment.Append("ml/min.");
                    }
                }//from if wt given
                else
                {
                    sbComment.Append("Can't caluculate Cockcroft-Gault estimate because ");
                    sbComment.Append("weight is not specified.");
                }
                sbComment.Append("\r\nThe older less accurate Cockcroft-Gault method ");
                sbComment.Append("was used in medication ");
                sbComment.Append("pharmacokinetic studies in the past, so is still often used ");
                sbComment.Append("for dosing medicines.  ");
                sbComment.Append("Since it is not adjusted for body size smaller people ");
                sbComment.Append("should have lower values.");
            }//from if adult
            else if ((age > 0) && (age < 18)) //is child
            {
                //balk for bad length
                if ((lengthCm < 10) || (lengthCm > 300))
                {
                    sbResult.Append("Length of ");
                    sbResult.Append(lengthCm.ToString());
                    sbResult.Append(" cm. is outside the calculable range.");
                }

                //(normal values come from UpToDate table in 2008)

                //1 wk
                if ((age > 0) && (age <= (2d / 52d)))
                {
                    sbFormula.Append("Pediatric Schwartz equation for 1 wk: ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append(".45 * length / creat IF TERM");
                    result_MDRD = .45 * lengthCm / creatinine;
                    sbResult.Append(string.Format("{0:#}", result_MDRD));
                    sbResult.Append(" ml/min/1.73 m^2");
                    sbResult.Append(" [nl 40.6]");
                }
                    //2-8 wk
                else if (age <= (8d / 52d))
                {
                    sbFormula.Append("Pediatric Schwartz equation 2-8 wk: ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append(".45 * length / creat ");
                    result_MDRD = .45 * lengthCm / creatinine;
                    sbResult.Append(string.Format("{0:#}", result_MDRD));
                    sbResult.Append(" ml/min/1.73 m^2");
                    sbResult.Append(" [nl 65.8]");
                }
                    //8 wk to 1 yr
                else if (age < 1d)
                {;
                    sbFormula.Append("Pediatric Schwartz equation 8wk-2yr: ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append(".45 * length / creat ");
                    result_MDRD = .45 * lengthCm / creatinine;
                    sbResult.Append(string.Format("{0:#}", result_MDRD));
                    sbResult.Append(" ml/min/1.73 m^2");
                    sbResult.Append(" [nl 95.7]");
                }
                    //1-12.9
                else if (age < 13d)
                {
                    sbFormula.Append("Pediatric Schwartz equation 2-12y: ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append(".55 * length / creat ");
                    result_MDRD = .55 * lengthCm / creatinine;
                    sbResult.Append(string.Format("{0:#}", result_MDRD));
                    sbResult.Append(" ml/min/1.73 m^2");
                    sbResult.Append(" [nl 133 if 2-12]");
                }
                    //males 13-21
                else if (!isFemale)
                {
                    sbFormula.Append("Pediatric Schwartz equation 13-21y male: ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append(".70 * length / creat ");
                    result_MDRD = .70 * lengthCm / creatinine;
                    sbResult.Append(string.Format("{0:#}", result_MDRD));
                    sbResult.Append(" ml/min/1.73 m^2");
                    sbResult.Append(" [nl 140]");
                }
                else
                {
                    //is female 13-21
                    sbFormula.Append("Pediatric Schwartz equation 13-21y female: ");
                    sbFormula.Append(Environment.NewLine);
                    sbFormula.Append(".55 * length / creat ");
                    result_MDRD = .55 * lengthCm / creatinine;
                    sbResult.Append(string.Format("{0:#}", result_MDRD));
                    sbResult.Append(" ml/min/1.73 m^2"); 
                    sbResult.Append(" [nl 126]");
                }
            }
                //outside range 0-120
            else
            {
                sbResult.Append("given age of ");
                sbResult.Append(age.ToString());
                sbResult.Append(" is outside calculatable range.");
            }
            

            result = sbResult.ToString();
            formula = sbFormula.ToString();
            comment = sbComment.ToString();
            return result_MDRD;
        }
    }
}
