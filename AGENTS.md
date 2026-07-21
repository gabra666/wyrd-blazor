# Project instructions

## Living game design document

`docs/GAME_DESIGN.md` is the source of truth for the current game design.

Whenever a task establishes, changes, rejects, or materially clarifies a gameplay decision:

1. Update the relevant section of `docs/GAME_DESIGN.md` in the same change.
2. Keep established decisions, provisional proposals, and open questions clearly separated.
3. Update the document version, last-review date, and revision history for significant changes.
4. Keep terminology in code and documentation consistent.
5. Do not silently turn provisional values into fixed rules; record the decision first.

## Architecture

- Keep game rules and simulation logic in `Wyrd.Core` without dependencies on Blazor or Unity.
- Treat `Wyrd.Web` as a prototyping and simulation interface over the core.
- Keep presentation-specific concerns outside the core library.
