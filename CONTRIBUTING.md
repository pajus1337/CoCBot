# Contributing Guidelines

Thank you for considering contributing to **CoCBot**! We welcome all improvements, bug fixes, and ideas.

---

## 📁 Branch Strategy

- `template` – Clean base architecture, useful for new projects
- `dev` – All development PRs should go here first
- `master` – Stable, tested releases only

---

## 🧪 Pull Request Workflow

1. **Fork** the repository
2. **Clone** your fork: `git clone your-fork-url`
3. **Create a new branch** off `dev`:
   ```bash
   git checkout -b feat/my-feature
   ```
4. **Commit using conventional commits** (see below)
5. **Push** to your fork and create a **Pull Request** targeting `dev`
6. Write a clear **description** of your change
7. Wait for review and feedback 🙌

---

## ✍️ Commit Message Format

We follow [Conventional Commits](https://www.conventionalcommits.org/):

```text
feat: short summary

- Bullet 1
- Bullet 2
```

Examples:

- `feat: add support for multi-emulator selection`
- `fix: resolve crash when no device is connected`
- `refactor: extract screenshot logic into VisionService`

---

## 💡 Coding Style

- Use meaningful variable names (no single-letter loops)
- Prefer async/await
- Use `init` over `set` when possible for options/config
- Keep responsibilities separated (SRP!)

---

## ✅ Ready to Merge?

A PR is considered ready when:

- All CI tests pass (coming soon)
- Code is reviewed and approved
- Branch is up-to-date with `dev`

After merging to `dev`, a maintainer will later promote the changes to `master`.

---

## 🙏 Thank You!

Your time, help and ideas are what make this bot better. We appreciate every contribution ❤️

