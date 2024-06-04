import { mapToi18nextSupportedLanguage, useTranslation } from "../i18n";
import axiosInstance from "../api/axiosInstance";
import Link from "next/link";
import Image from "next/image";
import Icon from "../images/icon.jpeg";

type HomeProps = {
  params: {
    lng: string;
  };
};

export default async function Home({ params: { lng } }: HomeProps) {
  let supportedLanguages: string[] = [];
  try {
    const response = await axiosInstance.get(
      "https:/localhost:7035/api/supported-languages"
    );
    supportedLanguages = response.data.map(mapToi18nextSupportedLanguage);
  } catch (error) {
    console.error(error);
  }
  const { t } = await useTranslation(lng);

  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-24">
      <div className="z-10 w-full max-w-5xl flex flex-row items-center justify-between font-mono text-sm lg:flex">
        <div className="dropdown inline-block relative">
          <button className="bg-gray-300 text-gray-700 font-semibold py-2 px-4 rounded inline-flex items-center">
            <span className="mr-1">{t('label.chooseLanguage')}</span>
            <svg
              className="fill-current h-4 w-4"
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 20 20"
            >
              <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />{" "}
            </svg>
          </button>
          <ul className="dropdown-menu absolute hidden text-gray-700 pt-1 group-hover:block">
            {supportedLanguages
              .filter((l) => lng !== l)
              .map((l, index) => {
                return (
                  <li className="w-28 text-center" key={l}>
                    <Link
                      className="rounded-t bg-gray-200 hover:bg-gray-400 py-2 px-4 block whitespace-no-wrap"
                      href={`/${l}`}
                    >
                      {l}
                    </Link>
                  </li>
                );
              })}
          </ul>
        </div>
        <Image
          src={Icon}
          alt="translord Logo"
          className=""
          width={100}
          height={100}
        />
      </div>

      <div className="relative z-[-1] flex flex-col text-center place-items-center before:absolute before:h-[300px] before:w-full before:-translate-x-1/2 before:rounded-full before:bg-gradient-radial before:from-white before:to-transparent before:blur-2xl before:content-[''] after:absolute after:-z-20 after:h-[180px] after:w-full after:translate-x-1/3 after:bg-gradient-conic after:from-sky-200 after:via-blue-200 after:blur-2xl after:content-[''] before:dark:bg-gradient-to-br before:dark:from-transparent before:dark:to-blue-700 before:dark:opacity-10 after:dark:from-sky-900 after:dark:via-[#0141ff] after:dark:opacity-40 sm:before:w-[480px] sm:after:w-[240px] before:lg:h-[360px]">
        <span>{t("message.paragraph1")} 😊</span>
        <br/>
        <span>{t("message.paragraph2")} 🏝️</span>
        <br/>
        <span>{t("message.paragraph3")} 🤖</span>
        <br/>
        <span>{t("message.paragraph4")} 📦</span>
        <br/>
        <span>{t("message.paragraph5")} 🫡</span>
        <br/>
        <span>{t("message.paragraph6")} ⭐️</span>
      </div>

      <div className="footer" />
    </main>
  );
}
