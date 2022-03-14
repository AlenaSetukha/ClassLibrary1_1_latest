// pch.cpp: файл исходного кода, соответствующий предварительно скомпилированному заголовочному файлу

#include "pch.h"

// При использовании предварительно скомпилированных заголовочных файлов необходим следующий файл исходного кода для выполнения сборки.
#include "pch.h"
#include "mkl.h"
#include <string.h>
#include <time.h>
#include <stdio.h>
#include <cmath>
#include <iostream>
#include <chrono>
#include <ctime> 


extern "C" _declspec(dllexport)
void Global_func(const double* v, const int n, double* res, const char* mode,
    double& v_time, int& ret, const int f)
{
    try
    {
        MKL_INT64 k = -1;

        if (!strcmp(mode, "VML_HA"))
        {
            k = VML_HA;
        }
        if (!strcmp(mode, "VML_EP"))
        {
            k = VML_EP;
        }

        if (k == -1)
        {
            //----------------------------Without MKL----------------------------------
            if (f == 0)//Tan
            {
                auto start = std::chrono::system_clock::now();
                for (int i = 0; i < n; i++)
                {
                    res[i] = tan(v[i]);
                }
                auto end = std::chrono::system_clock::now();
                std::chrono::duration<double> elapsed_seconds = end - start;
                v_time = elapsed_seconds.count();
            }
            else
            {
                auto start = std::chrono::system_clock::now();
                for (int i = 0; i < n; i++)
                {
                    res[i] = erf(v[i]);
                }
                auto end = std::chrono::system_clock::now();
                std::chrono::duration<double> elapsed_seconds = end - start;
                v_time = elapsed_seconds.count();
            }
        }
        //------------------------------Using MKL----------------------------------
        else
        {
            if (f == 0)//Tan
            {
                //n - number of elements, v - array, res - result, k - mode option
                auto start = std::chrono::system_clock::now();
                vmdTan(n, v, res, k);
                auto end = std::chrono::system_clock::now();
                std::chrono::duration<double> elapsed_seconds = end - start;
                v_time = elapsed_seconds.count();

            }
            else
            {
                //n - number of elements, v - array, res - result, k - mode option
                auto start = std::chrono::system_clock::now();
                vmdErfInv(n, v, res, k);
                auto end = std::chrono::system_clock::now();
                std::chrono::duration<double> elapsed_seconds = end - start;
                v_time = elapsed_seconds.count();
            }
        }
        ret = 0;
    }
    catch (...)
    {
        ret = -1;
    }
}